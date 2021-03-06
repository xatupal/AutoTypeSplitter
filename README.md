# AutoTypeSplitter
KeePass Plugin

Splits auto-typing into distinct parts.

It is useful when the login screen consists of multiple parts and it is not known in advance when or what characters will be needed. Also, the plugin can be used as a delay method until the next page of a multipage login loads and when a particular entry is to be entered in multiple places with varying arrangements/designs of input fields.

Example usages:
* `{USERNAME}{ENTER}{SPLIT}{PICKCHARS}{ENTER}` - KeePass will print the username and **then** will show the pickchars window.
* `{USERNAME}{ENTER}{SPLIT:Custom message}{PASSWORD}{ENTER}` - KeePass will print the username and **then** will show a message box with _Custom message_:
  * If the user clicks _OK_ button or presses _Enter_, KeePass will continue auto-typing and print the password.
  * If the user clicks _Cancel_ or presses _Esc_, KeePass will break auto-typing.
* Multiple usages of `{SPLIT}` within a sequence is allowed e.g.:
  * `{USERNAME}{ENTER}{SPLIT}{PICKFIELD}{ENTER}{SPLIT:Custom message}{S:CustomField}{ENTER}`

The plugin is compatible with [TCATO](https://keepass.info/help/v2/autotype_obfuscation.html) and can extend the functionality of  [TCATO Placeholder](https://keepass.info/plugins.html#tcatoplh) plugin, e.g.:
* `{TCATO:false}{USERNAME}{TAB}{SPLIT}{TCATO:true}{PASSWORD}` - KeePass will enable TCATO for the password only.
  
Whereas, if all you need is just to postpone `{PICKCHARS}` window then it's easier to use [PickCharsDeffered](https://github.com/xatupal/PickCharsDeferred) plugin which does not require any changes in the sequences.

### Installation

Copy ![AutoTypeSplitter.dll](https://github.com/xatupal/AutoTypeSplitter/releases/latest/download/AutoTypeSplitter.dll) or ![AutoTypeSplitter.plgx](https://github.com/xatupal/AutoTypeSplitter/releases/latest/download/AutoTypeSplitter.plgx) to the KeePass Plugins directory or install via ![Chocolatey](https://chocolatey.org):

```
choco install keepass-plugin-autotypesplitter
```
