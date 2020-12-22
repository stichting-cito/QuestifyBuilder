set publicationpath="<path-to-publicaties-folder>"
forfiles -p %publicationpath% -s -d -1 -c "cmd /c IF @isdir == TRUE rd /S /Q @path"
forfiles -p %publicationpath% -s -d -1 -c "cmd /c IF @isdir == FALSE del @path"

