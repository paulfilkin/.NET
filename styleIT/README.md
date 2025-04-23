# styleIT - XML to HTML Transformer

**styleIT** is a simple desktop application that transforms XML files into HTML using XSL (Extensible Stylesheet Language) transformations. It provides a user-friendly interface for anyone to apply XSL stylesheets to XML data and view the resulting HTML output directly in a browser.  I built it for fun to see what I could do with no knowledge of developing in this language at all, but it's also quite handy :-)

<img src=".\IMAGES\styleIT_UI.png" style="width: 600px; float: left; margin-right: 15px;" alt="styleIT UI">

## Features
- Drag-and-drop support for XML and XSL files.
- File selection dialogs for manual XML and XSL file selection.
- Transform XML to HTML with a single click.
- Save the transformed HTML output to a file.
- View the generated HTML source code in a built-in viewer.
- Lightweight and easy to use with a clean UI.

## Usage
1. **Run the Application**:
   - In Visual Studio: Press `F5` to run the project in Debug mode.
   - From the command line:
     ```bash
     dotnet run --project styleIT
     ```
     
   - Or download the compiled zip <a href="https://raw.githubusercontent.com/paulfilkin/.NET/main/styleIT/styleIT_compiled.zip" target="_blank">styleIT_compiled.zip</a>, then unzip it to somewhere on your computer (I used Program Files) and then create a shortcut to styleIT.exe.
   
2. **Transform XML to HTML**:
   
   - Launch the application to see the main window.
   - Drag and drop an XML file and an XSL file into the drag-and-drop area, or use the "Select XML" and "Select XSL" buttons to choose files manually.
   - Click the "Transform" button to generate the HTML output, which will open in your default browser.
   - Use the "Save Output" button to save the HTML to a file.
   - Use the "View Source" button to view the generated HTML source code.
   
3. **Example Files**:
   - You can test the application with sample files:
     - <a href="https://github.com/paulfilkin/.NET/blob/main/styleIT/styleIT/Samples/book_library.xml" target="_blank">library.xml</a> : An XML file containing book data.
     - <a href="https://github.com/paulfilkin/.NET/main/styleIT/styleIT/Samples/book_catalog.xsl" target="_blank">book_catalog.xsl</a>: An XSL stylesheet to transform the XML into an HTML catalog.
   - Place these files in the project directory, then drag and drop them into the application to see the transformation.

## Contributing
Contributions are welcome! Please ensure your code includes comments where necessary. If you’re adding new features, consider including tests or example files to demonstrate the functionality.
## Acknowledgments
- A ChatGPT/Grok creation :-)
