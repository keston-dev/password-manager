# Password Manager

My capstone project for my advanced web design class.

This project will be updated periodically to reflect changes done in "sprints". Once I'm fully finished, there may be some addt. features implemented post-presentation, then ill publish this project. The README will be updated to reflect when this project is completed.

## Tools/Stack Used

(I will update this if I add more stuff)

- C#
- ASP.NET Core (MVC)
- TailwindCSS
- MSSQL
- EF Core

## TODO / some planned features

- [ ] "User"/"Account" model
- [ ] Data retention and encryption
- [ ] Security questions?
- [ ] Password strength testing
- [ ] Password re-use monitor
- [ ] Password auto-generation

## Building/testing

If you wish to build or test this project, either before its fully finished or after, the following steps are needed:

1. Clone the repository

```bash
git clone https://github.com/keston-dev/password-manager.git
```

2. Configure the database

   This project uses the [Microsoft SQL Server DB](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (as well as its EF core package), but you're welcome to change this.
   You'll also need to create your own version of the `appsettings.json` file, which I have provided an `appsettings.example.json` file to build from. It already contains a local DB that you can run without any configuration necessary, or you can bring your own.

3. Install dependencies (optional)

   This comes from Tailwind specifically. You'll need [NodeJS](https://nodejs.org/en) installed.
   Run:

   ```bash
   npm i
   ```

   This is only necessary if you wish to update the `tailwind.css` file based on [Tailwind's CLI install guide](https://tailwindcss.com/docs/installation/tailwind-cli) with a `--watch` option. If you wish to modify the existing css or just use whats already available, you don't need to do this, and can instead just edit `wwwroot/css/tailwind.css`

4. Build the project

   If youre using Visual Studio, you should be able to build it as any other .NET application.

   However, if you're like me, and not running this in visual studio, I've provided some VSCode launch and tasks configured:
   1. Press `CTRL+SHIFT+D` or click the `Run and Debug` option on the sidebar
   2. For live testing, run the `.NET Core Watch (web)` option, for general dev testing with no live updates, run the `.NET Core Launch (web)` option. Both will build the `bin` files needed to run the app.
