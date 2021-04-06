// ***********************************************************************
// Assembly         : Xml2CSharp.Clent
// Author           : msyoung
// Created          : 08-04-2018
//
// Last Modified By : ravensorb
// Last Modified On : 08-04-2018
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;

namespace Xml2CSharp.Clent
{
    /// <summary>
    /// Class Program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                return;
            }

            var xml = File.ReadAllText(options.XmlFile);

            var classInfo = new Xml2CSharpConverer().Convert(xml);

            var classInfoWriter = new ClassInfoWriter(classInfo, options.CustomNameSpace);

            if (string.IsNullOrEmpty(options.CSharpFileName))
            {
                classInfoWriter.WriteHeader(Console.Out);
                classInfoWriter.Write(Console.Out);
                classInfoWriter.WriteFooter(Console.Out);
            }

            if (!string.IsNullOrEmpty(options.CSharpFileName))
            {
                using (var sw = new StreamWriter(File.OpenWrite(options.CSharpFileName)))
                {
                    classInfoWriter.WriteHeader(sw);
                    classInfoWriter.Write(sw);
                    classInfoWriter.WriteFooter(sw);
                }
            }
        }
    }

    /// <summary>
    /// Class Options.
    /// </summary>
    internal class Options
    {
        /// <summary>
        /// Gets or sets the XML file.
        /// </summary>
        /// <value>The XML file.</value>
        [Option('x', "xmlFile", Required = true,
          HelpText = "Xml file used to create c# files.")]
        public string XmlFile { get; set; }

        /// <summary>
        /// Gets or sets the name of the c sharp file.
        /// </summary>
        /// <value>The name of the c sharp file.</value>
        [Option('c', "cSharpFileName", DefaultValue = null,
          HelpText = "Name of C# file to be created")]
        public string CSharpFileName { get; set; }

        /// <summary>
        /// Gets or sets the custom name space.
        /// </summary>
        /// <value>The custom name space.</value>
        [Option('n', "namespace", DefaultValue = null,
            HelpText = "Custom Namespace to use in output class files")]
        public string CustomNameSpace { get; set; }

        /// <summary>
        /// Gets or sets the last state of the parser.
        /// </summary>
        /// <value>The last state of the parser.</value>
        [ParserState]
        public IParserState LastParserState { get; set; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <returns>System.String.</returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
