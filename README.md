# Password Manager

My capstone project for my advanced web design class.

This project will be updated periodically to reflect changes done in "sprints". Once I'm fully finished, there may be some addt. features implemented post-presentation, then ill publish this project. The README will be updated to reflect when this project is completed.

## Tools/Stack Used

(I will update this if I add more stuff)

- .NET 8
- ASP.NET Core (MVC)
- TailwindCSS
- MSSQL
- EF Core
- Google OAuth
- ZXCVBN (for password strength testing)

Passwords are encrypted using the AES-256 specification.

## TODO / some planned features

- [x] Account model
- [x] Data retention and encryption
- [ ] Security questions?
- [x] Password strength testing
- [x] Password re-use monitor
- [x] Password auto-generation
- [x] Oauth account connection.

## Building/testing

If you wish to build or test this project, either before its fully finished or after, the following steps are needed:

1. Clone the repository

```bash
git clone https://github.com/keston-dev/password-manager.git
```

2. Configure the database

   This project uses the [Microsoft SQL Server DB](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (as well as its EF core package), but you're welcome to change this.
   You'll also need to create your own version of the `appsettings.json` file, which I have provided an `appsettings.example.json` file to build from. It already contains a local DB that you can run without any configuration necessary, or you can bring your own.

3. Install dependencies
   This is required for tailwind and zxcvbn (which is the [`zxcvbn-ts` package on NPM](https://www.npmjs.com/package/zxcvbn-ts)). You'll need [NodeJS](https://nodejs.org/en) installed.
   Run:

   ```bash
   npm i
   ```

   If you wish to modify the existing css or just use whats already available, you can instead just edit `wwwroot/css/tailwind.css`.

4. Configure `appsettings`
   As mentioned, I've given a template for the `appsettings.json`, which originally had default values. However there are more keys now you must get:
   1. For Google's Oauth, you must obtain a client ID and client secret, located at [Google's Cloud Console](https://console.cloud.google.com/welcome)
   2. Create a new project, then navigate on the sidebar to `APIs & Services` -> `Credentials` -> create an `OAuth 2.0 Client Ids`, select type `Web Application`, and copy the `Client Id` and `Client Secret`.
   3. For the encryption key, you can generate a pseudo-random one with the following snippet:

   ```cs
     var key = new byte[32];
     /**System.Security.Cryptography.*/ RandomNumberGenerator.Fill(key);
     Console.WriteLine(Convert.ToBase64String(key));
   ```

5. Build the project

   If youre using Visual Studio, you should be able to build it as any other .NET application.

   However, if you're like me, and not running this in visual studio, I've provided some VSCode launch and tasks configured:
   1. Press `CTRL+SHIFT+D` or click the `Run and Debug` option on the sidebar
   2. For live testing, run the `.NET Core Watch (web)` option, for general dev testing with no live updates, run the `.NET Core Launch (web)` option. Both will build the `bin` files needed to run the app.
