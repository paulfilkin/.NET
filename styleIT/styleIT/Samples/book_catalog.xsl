<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/library">
    <html lang="en">
      <head>
        <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <title>Modern Book Library</title>
        <script src="https://cdn.tailwindcss.com"></script>
        <style>
          .book-card { transition: transform 0.3s ease, box-shadow 0.3s ease; }
          .book-card:hover { transform: translateY(-5px); box-shadow: 0 10px 20px rgba(0,0,0,0.2); }
          .rating-star { color: #FFD700; }
        </style>
      </head>
      <body class="bg-gradient-to-br from-purple-100 via-pink-100 to-blue-100 min-h-screen">
        <div class="container mx-auto p-6">
          <h1 class="text-4xl font-bold text-center text-purple-800 mb-8">Modern Book Library</h1>

          <!-- Filter and Sort Controls -->
          <div class="flex justify-between items-center mb-6 bg-white p-4 rounded-lg shadow">
            <div>
              <label class="text-gray-700 font-semibold mr-2">Filter by Category:</label>
              <select id="categoryFilter" onchange="filterBooks()" class="p-2 rounded border">
                <option value="all">All</option>
                <xsl:for-each select="book/category[not(.=preceding::category)]">
                  <option value="{.}"><xsl:value-of select="."/></option>
                </xsl:for-each>
              </select>
            </div>
            <div>
              <label class="text-gray-700 font-semibold mr-2">Sort by:</label>
              <select id="sortSelect" onchange="sortBooks()" class="p-2 rounded border">
                <option value="title">Title</option>
                <option value="year">Year</option>
                <option value="rating">Rating</option>
              </select>
            </div>
          </div>

          <!-- Book Grid -->
          <div id="bookGrid" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            <xsl:for-each select="book">
              <xsl:sort select="title"/>
              <div class="book-card bg-white rounded-lg shadow-lg p-6 category-{category}">
                <h2 class="text-xl font-semibold text-purple-700 mb-2"><xsl:value-of select="title"/></h2>
                <p class="text-gray-600 mb-1"><strong>Author:</strong> <xsl:value-of select="author"/></p>
                <p class="text-gray-600 mb-1"><strong>Year:</strong> <xsl:value-of select="year"/></p>
                <p class="text-gray-600 mb-1"><strong>Category:</strong> <xsl:value-of select="category"/></p>
                <p class="text-gray-600 mb-2"><strong>Rating:</strong>
                  <xsl:call-template name="ratingStars">
                    <xsl:with-param name="rating" select="rating"/>
                  </xsl:call-template>
                </p>
                <p class="text-gray-500 text-sm"><xsl:value-of select="description"/></p>
              </div>
            </xsl:for-each>
          </div>
        </div>

        <!-- JavaScript for Filtering and Sorting -->
        <script>
          function filterBooks() {
            const filter = document.getElementById('categoryFilter').value;
            const books = document.querySelectorAll('#bookGrid .book-card');
            books.forEach(book => {
              if (filter === 'all' || book.classList.contains('category-' + filter)) {
                book.style.display = 'block';
              } else {
                book.style.display = 'none';
              }
            });
          }

          function sortBooks() {
            const sortBy = document.getElementById('sortSelect').value;
            const bookGrid = document.getElementById('bookGrid');
            const books = Array.from(bookGrid.querySelectorAll('.book-card'));
            books.sort((a, b) => {
              let aValue, bValue;
              if (sortBy === 'title') {
                aValue = a.querySelector('h2').textContent;
                bValue = b.querySelector('h2').textContent;
              } else if (sortBy === 'year') {
                aValue = parseInt(a.querySelector('p:nth-child(3)').textContent.split(': ')[1]);
                bValue = parseInt(b.querySelector('p:nth-child(3)').textContent.split(': ')[1]);
              } else if (sortBy === 'rating') {
                aValue = a.querySelectorAll('.rating-star').length;
                bValue = b.querySelectorAll('.rating-star').length;
              }
              return aValue > bValue ? 1 : -1;
            });
            bookGrid.innerHTML = '';
            books.forEach(book => bookGrid.appendChild(book));
          }
        </script>
      </body>
    </html>
  </xsl:template>

  <!-- Template for Rendering Rating Stars -->
  <xsl:template name="ratingStars">
    <xsl:param name="rating"/>
    <xsl:if test="$rating > 0">
      <span class="rating-star">★</span>
      <xsl:call-template name="ratingStars">
        <xsl:with-param name="rating" select="$rating - 1"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>