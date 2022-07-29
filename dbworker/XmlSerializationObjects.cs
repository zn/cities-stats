// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Yandexsearch));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Yandexsearch)serializer.Deserialize(reader);
// }


// https://json2csharp.com/code-converters/xml-to-csharp

using System.Xml.Serialization;

namespace SmallCities.Serialization;

[XmlRoot(ElementName="sortby")]
public class Sortby { 

	[XmlAttribute(AttributeName="order")] 
	public string Order { get; set; } 

	[XmlAttribute(AttributeName="priority")] 
	public string Priority { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="groupby")]
public class Groupby { 

	[XmlAttribute(AttributeName="attr")] 
	public string Attr { get; set; } 

	[XmlAttribute(AttributeName="mode")] 
	public string Mode { get; set; } 

	[XmlAttribute(AttributeName="groups-on-page")] 
	public int GroupsOnPage { get; set; } 

	[XmlAttribute(AttributeName="docs-in-group")] 
	public int DocsInGroup { get; set; } 

	[XmlAttribute(AttributeName="curcateg")] 
	public int Curcateg { get; set; } 
}

[XmlRoot(ElementName="groupings")]
public class Groupings { 

	[XmlElement(ElementName="groupby")] 
	public Groupby Groupby { get; set; } 
}

[XmlRoot(ElementName="request")]
public class Request { 

	[XmlElement(ElementName="query")] 
	public string Query { get; set; } 

	[XmlElement(ElementName="page")] 
	public int Page { get; set; } 

	[XmlElement(ElementName="sortby")] 
	public Sortby Sortby { get; set; } 

	[XmlElement(ElementName="maxpassages")] 
	public int Maxpassages { get; set; } 

	[XmlElement(ElementName="groupings")] 
	public Groupings Groupings { get; set; } 
}

[XmlRoot(ElementName="found")]
public class Found { 

	[XmlAttribute(AttributeName="priority")] 
	public string Priority { get; set; } 

	[XmlText] 
	public int Text { get; set; } 
}

[XmlRoot(ElementName="page")]
public class Page { 

	[XmlAttribute(AttributeName="first")] 
	public int First { get; set; } 

	[XmlAttribute(AttributeName="last")] 
	public int Last { get; set; } 

	[XmlText] 
	public int Text { get; set; } 
}

[XmlRoot(ElementName="relevance")]
public class Relevance { 

	[XmlAttribute(AttributeName="priority")] 
	public string Priority { get; set; } 
}

[XmlRoot(ElementName="title")]
public class Title { 

	[XmlElement(ElementName="hlword")] 
	public string Hlword { get; set; } 
}

[XmlRoot(ElementName="passage")]
public class Passage { 

	[XmlElement(ElementName="hlword")] 
	public List<string> Hlword { get; set; } 
}

[XmlRoot(ElementName="passages")]
public class Passages { 

	[XmlElement(ElementName="passage")] 
	public List<Passage> Passage { get; set; } 
}

[XmlRoot(ElementName="properties")]
public class Properties { 

	[XmlElement(ElementName="_PassagesType")] 
	public int PassagesType { get; set; } 

	[XmlElement(ElementName="lang")] 
	public string Lang { get; set; } 
}

[XmlRoot(ElementName="doc")]
public class Doc { 

	[XmlElement(ElementName="relevance")] 
	public Relevance Relevance { get; set; } 

	[XmlElement(ElementName="saved-copy-url")] 
	public string Savedcopyurl { get; set; } 

	[XmlElement(ElementName="url")] 
	public string Url { get; set; } 

	[XmlElement(ElementName="domain")] 
	public string Domain { get; set; } 

	[XmlElement(ElementName="title")] 
	public Title Title { get; set; } 

	[XmlElement(ElementName="modtime")] 
	public string Modtime { get; set; } 

	[XmlElement(ElementName="size")] 
	public int Size { get; set; } 

	[XmlElement(ElementName="charset")] 
	public string Charset { get; set; } 

	[XmlElement(ElementName="mime-type")] 
	public string Mimetype { get; set; } 

	[XmlElement(ElementName="passages")] 
	public Passages Passages { get; set; } 

	[XmlElement(ElementName="properties")] 
	public Properties Properties { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public string Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 

	[XmlElement(ElementName="headline")] 
	public string Headline { get; set; } 
}

[XmlRoot(ElementName="group")]
public class Group { 

	[XmlElement(ElementName="doccount")] 
	public int Doccount { get; set; } 

	[XmlElement(ElementName="relevance")] 
	public Relevance Relevance { get; set; } 

	[XmlElement(ElementName="doc")] 
	public Doc Doc { get; set; } 
}

[XmlRoot(ElementName="found-docs")]
public class Founddocs { 

	[XmlAttribute(AttributeName="priority")] 
	public string Priority { get; set; } 

	[XmlText] 
	public int Text { get; set; } 
}

[XmlRoot(ElementName="grouping")]
public class Grouping { 

	[XmlElement(ElementName="page")] 
	public Page Page { get; set; } 

	[XmlElement(ElementName="group")] 
	public List<Group> Group { get; set; } 

	[XmlElement(ElementName="found")] 
	public List<Found> Found { get; set; } 

	[XmlElement(ElementName="founddocs")] 
	public List<Founddocs> Founddocs { get; set; } 

	[XmlElement(ElementName="found-docs-human")] 
	public string Founddocshuman { get; set; } 

	[XmlAttribute(AttributeName="attr")] 
	public string Attr { get; set; } 

	[XmlAttribute(AttributeName="mode")] 
	public string Mode { get; set; } 

	[XmlAttribute(AttributeName="groups-on-page")] 
	public int GroupsOnPage { get; set; } 

	[XmlAttribute(AttributeName="docs-in-group")] 
	public int DocsInGroup { get; set; } 

	[XmlAttribute(AttributeName="curcateg")] 
	public int Curcateg { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="results")]
public class Results { 

	[XmlElement(ElementName="grouping")] 
	public Grouping Grouping { get; set; } 
}

[XmlRoot(ElementName="error")]
public class Error { 

	[XmlAttribute(AttributeName="code")] 
	public int Code { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="response")]
public class Response { 

    [XmlElement(ElementName="error")] 
	public Error Error { get; set; } 
    
	[XmlElement(ElementName="reqid")] 
	public string Reqid { get; set; } 

	[XmlElement(ElementName="found")] 
	public List<Found> Found { get; set; } 

	[XmlElement(ElementName="found-human")] 
	public string Foundhuman { get; set; } 

	[XmlElement(ElementName="is-local")] 
	public string Islocal { get; set; } 

	[XmlElement(ElementName="results")] 
	public Results Results { get; set; } 

	[XmlAttribute(AttributeName="date")] 
	public string Date { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="yandexsearch")]
public class Yandexsearch { 

	[XmlElement(ElementName="request")] 
	public Request Request { get; set; } 

	[XmlElement(ElementName="response")] 
	public Response Response { get; set; } 

	[XmlAttribute(AttributeName="version")] 
	public double Version { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

