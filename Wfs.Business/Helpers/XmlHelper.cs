using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Wfs.Business.Helpers
{
    public interface IXmlHelper
    {
        bool WriteToXml<TModel>(TModel model, string filePath, string fileName)
            where TModel : class;

        TModel ReadFromXml<TModel>(string filePath, string fileName)
            where TModel : class;

        FileInfo[] GetXmlFiles(string filePath, string filePreFix);

        bool MoveXmlFile(string fromFilePath, string toFilePath, string fileName);
    }

    public class XmlHelper : IXmlHelper
    {
        public FileInfo[] GetXmlFiles(string filePath, string filePreFix)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            return directoryInfo.GetFiles(filePreFix + "*.xml");
        }

        public bool MoveXmlFile(string fromFilePath, string toFilePath, string fileName)
        {
            bool fileMoveStatus;
            try
            {
                fromFilePath += "\\" + fileName + ".xml";
                toFilePath += "\\" + fileName + ".xml";
                File.Move(fromFilePath, toFilePath);
                fileMoveStatus = true;
            }catch (Exception ex)
            {
                // TODO Logger
                fileMoveStatus = false;
            }

            return fileMoveStatus;
        }

        public TModel ReadFromXml<TModel>(string filePath, string fileName)
            where TModel : class
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(TModel));
                var file = new StreamReader($"{filePath}\\{fileName}");
                var modelData = (TModel)reader.Deserialize(file);
                file.Close();
                return modelData;
            }
            catch (Exception ex)
            {
                // TODO Logger
                return null;
            }
        }

        public bool WriteToXml<TModel>(TModel model, string filePath, string fileName) where TModel : class
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            try
            {
                var path = $"{filePath}\\{fileName}";
                var file = File.Create(path);
                var writer = new XmlSerializer(typeof(TModel));
                writer.Serialize(file, model, ns);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                // TODO logger
                return false;
            }
        }
    }
}
