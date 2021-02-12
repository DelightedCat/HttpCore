**NOTE:** This project will soon be renamed to HttpCore in favor of just WebServer.

# WebServer
A small [HTTP server](https://en.wikipedia.org/wiki/Web_server) written in C# on
[.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0).

**NOTE:** This project has **not** been tested in a production environment, I do not
recommend using it as such!

## Getting Started
I've made this project to get a better understand of the working of TCP sockets in
C# and how web servers handle requests.

Here's how it works:

- The browser sends a request to the web server;
- The HTTP server reads and parses the incoming headers and body;
- The server will generate a response with all headers and body;
- This response is sent back to the browser;
- The browser will display the received response in the form of a web page!

In order to get working on this project, you'll need to have
[.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) installed.

### What's what?
The program's entry point is located in the `Program.cs` file.
It will generate a configuration file which will be written to the `data/config.json` file.

By default, the document root is located in the `www` directory of the build folder.
Try placing a file called `index.html` in there and visit `localhost` while the server is up and running!

## Contributing
In case you want to contribute to this project, feel free to do so!
Keep in mind that there are some guidelines though.

- Use explicit type declarations and avoid using `var`;
- Use tabs of 4 spaces wide. That's right, no spaces! The `.editorconfig` should handle this for you though;
- Try to make as few changes to files such as `.gitignore` and `.editorconfig` as possible;

Issues are welcome to be opened in the issues section of this repository.
I'll do my very best to squish any bugs I encounter!
