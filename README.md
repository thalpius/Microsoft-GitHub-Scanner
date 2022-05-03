# Microsoft GitHub Scanner

I created a small application to scan a GitHub repository for a keyword. During a Microsoft webinar where I demonstrated a new feature within Microsoft Azure Identity Protection, multiple people requested the tool, so I decided to release it.

The tool is far from perfect as I'm no developer by any means. You can fork it, change it, create a pull request, or whatever you like to do, but I have no plans to change the tool as it was for demonstration purposes only.

**Note**: I created a release as a single .NET core file to download, so you don't have to compile it yourself.

# Usage Microsoft GitHub Scanner

Run the executable with the following arguments:

```cmd
/user:
/repo:
/branch:
/keyword:
```

Here is an example:

```cmd
MicrosoftGitHubScanner.exe /user:thalpius /repo:Microsoft-ADFS-Info /branch:master /keyword:ConfigurationDatabaseConnectionString
```

# Screenshot

![Alt text](/Screenshots/MicrosoftGitHubScanner01.png?raw=true "Microsoft GitHub Scanner")
