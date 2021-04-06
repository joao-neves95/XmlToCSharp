XmlToCSharp
===========

Based on the site for converting XML into XmlSerializer compatible C# classes.

http://xmltocsharp.azurewebsites.net/

## Example of using in application


```
var xml = File.ReadAllText("test.xml");

var classInfo = new Xml2CSharpConverer().Convert(xml);

var classInfoWriter = new ClassInfoWriter(classInfo);

classInfoWriter.Write(Console.Out);

using (var sw = new StreamWriter(File.OpenWrite("test.cs")))
{
	classInfoWriter.Write(sw);
}
```
