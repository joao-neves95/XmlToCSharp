XmlToCSharp
===========

This is based off of the original code used to create the Site for Converting XML into XmlSerializer compatable C# Classes

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
