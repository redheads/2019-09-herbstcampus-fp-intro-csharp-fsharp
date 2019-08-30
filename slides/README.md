- Framework: [reveal-md](https://github.com/webpro/reveal-md)
- PDF export: [desktape](https://github.com/astefanutti/decktape)

# Initial setup

- `npm install -g reveal-md`
- `npm install -g decktape`
- install [FiraCode](https://github.com/tonsky/FiraCode) system-wide

# Normal usage

## Starting the slide show

### Starting the workshop presentation

- `npm start`

### Starting the 'normal' presentation

- `npm run vortrag`

## Exporting PDF from slide show

- start the slide show (must be active!): `npm start`
- in a different shell: `npm run pdf`

(adapt for `vortrag` version accordingly: `npm run vortrag` and `npm run pdf-vortrag`)

On Arch Linux the `decktape` command requires the CLI option `--chrome-arg=--no-sandbox` (for details see "Errors - No usable sandbox!" section in https://github.com/astefanutti/decktape)

# Reveal-md basics

`reveal-md` is a convenience wrapper around `reveal-js` for people who prefer using markdown instead of plain html. The main advantages is that you only have your content in the Git repository (and none of the reveal-js framework).

The basic command for starting a `reveal-md` presentation is `reveal-md your-content.md`.

The presentation can be customized:

- from the command line
- from the header section in the markdown file
- from a template file (i.e. reveal.html)

Customization options include:

- css
- reveal configs (i.e. plugins)
- reveal themes

# Project structure

- `reveal.html`: reveal-md format of the standard `index.html` used by reveal-js: include reveal-plugins here
- `package.json`: includes scripts for starting presentation and creating pdf
- `content.md`: obvious...
- `custom.css`: obvious...

# Splitting single presentation into multiple md files

[Split single presentation in multiple .md files?](https://github.com/webpro/reveal-md/issues/228)

- Option 1: using `cat` (or similar tool): `cat demo.md content2.md > final-demo.md && reveal-md final-demo.md -w --template reveal.html`. Works with Windows10 using git-bash shell.
- Option 2: [using a custom preprocessor](https://github.com/webpro/reveal-md/issues/228#issuecomment-464028190):

Save the following as `preproc.js`:

```js
const { readFileSync } = require('fs');
const path = require('path');

const LINE_SEPARATOR = '\r\n';
const FILE_REF_REGEX = /^FILE: (.+)$/;

const isFileReference = (line) => FILE_REF_REGEX.test(line);
const loadFileContent = (line, basePath) => {
    const filePath = line.match(FILE_REF_REGEX)[1];
    return readFileSync(path.join(basePath, filePath));
};

const preprocess = async (markdown, options) =>
    markdown
        .split(LINE_SEPARATOR)
        .map(line => 
                isFileReference(line) 
                    ? loadFileContent(line, options.initialDir) 
                    : line)
        .join(LINE_SEPARATOR);

module.exports = preprocess;
```

Usage:

```
reveal-md -P preproc.js demo-index.md
```

Although this would be the more elegant solution, I have not managed to get this working yet (tried with Win10 in git-bash shell; also tried `\r\n` vs `\n`).
