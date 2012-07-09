using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using EvoqueMyStyle.DataAccess.Tables;

namespace EvoqueMyStyle.Website
{
    /// <summary>
    /// 用于XML输出的静态类。
    /// </summary>
    public static class XMLOutput
    {
        /// <summary>
        /// 将准备返回的数据转换成XML并输出到响应流（通常用于返回状态信息）。
        /// </summary>
        /// <param name="value">状态描述。</param>
        /// <param name="code">状态代码。</param>
        public static void ReturnValue(string value, string code)
        {
            ReturnValue(value, code, "error");
        }
        /// <summary>
        /// 将准备返回的数据转换成XML并输出到响应流。
        /// </summary>
        /// <param name="value">值。</param>
        /// <param name="code">代码。</param>
        /// <param name="type">标签。</param>
        public static void ReturnValue(string value, string code, string type)
        {
            XmlTextWriter xw = new XmlTextWriter(HttpContext.Current.Response.Output);
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 2;
            xw.WriteStartDocument();
            xw.WriteStartElement("return");

            xw.WriteStartElement("code");
            xw.WriteString(code);
            xw.WriteEndElement();
            xw.WriteStartElement(type);
            xw.WriteString(value);
            xw.WriteEndElement();

            xw.WriteEndElement();
            xw.Close();

            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 将准备返回的键值集合转换成XML列表并输出到响应流。
        /// </summary>
        /// <param name="value">键值集合。</param>
        /// <param name="type">标签名。</param>
        public static void ReturnValue(Dictionary<string, string> value, string type)
        {
            XmlTextWriter xw = new XmlTextWriter(HttpContext.Current.Response.Output);
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 2;
            xw.WriteStartDocument();
            xw.WriteStartElement("return");

            xw.WriteStartElement("code");
            xw.WriteString(string.Empty);
            xw.WriteEndElement();
            xw.WriteStartElement(type);
            foreach (var item in value)
            {
                xw.WriteStartElement("item");
                xw.WriteAttributeString("value", item.Key);
                xw.WriteString(item.Value);
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.Close();

            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 将返回的数据转换为XML并输出到响应流。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total"></param>
        public static void ReturnPicsList(IList<share> value, int total)
        {
            XmlTextWriter xw = new XmlTextWriter(HttpContext.Current.Response.Output);
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 2;
            xw.WriteStartDocument();
            xw.WriteStartElement("return");

            xw.WriteStartElement("total");
            xw.WriteValue(total);
            xw.WriteEndElement();
            xw.WriteStartElement("list");
            foreach (share item in value)
            {
                xw.WriteStartElement("item");

                xw.WriteElementString(share.Columns.id, item.Id.ToString());
                xw.WriteElementString(share.Columns.sina_uid, item.Sina_uid);
                xw.WriteElementString(share.Columns.sina_name, item.Sina_name);
                xw.WriteStartElement(share.Columns.comment);
                xw.WriteCData(item.Comment);
                xw.WriteEndElement();
                xw.WriteElementString(share.Columns.img, item.Img);
                xw.WriteElementString(share.Columns.type, item.Type);

                xw.WriteEndElement();
            }
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.Close();

            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 将指定的文本写入CDATA块中并作为XML输出到响应流。
        /// </summary>
        /// <param name="value">CDATA块中的文本。</param>
        /// <param name="code">状态代码。</param>
        /// <param name="type">CDATA块名称。</param>
        public static void ReturnCData(string value, string code, string type)
        {
            XmlTextWriter xw = new XmlTextWriter(HttpContext.Current.Response.Output);
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 2;
            xw.WriteStartDocument();
            xw.WriteStartElement("return");

            xw.WriteStartElement("code");
            xw.WriteString(code);
            xw.WriteEndElement();
            xw.WriteStartElement(type);
            xw.WriteCData(value);
            xw.WriteEndElement();

            xw.WriteEndElement();
            xw.Close();

            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.End();
        }
    }
}
