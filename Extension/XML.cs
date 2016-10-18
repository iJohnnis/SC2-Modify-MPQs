using System.Collections.Generic;
using System.Xml;


public static class XmlExtensions {
	#region Document
	public static XmlElement Id(this XmlDocument self, string id) {
		return self.GetElementById(id);
	}
	public static XmlDocument Load(this XmlDocument self, string filePath) {
		self.Load(filePath);
		return self;
	}
	#endregion

	#region Element value
	#region Get value of a child
	/// <summary> Equivalent to the XElement.Element </summary>
	public static XmlElement E(this XmlElement self, string tag) {
		foreach (XmlNode node in self.ChildNodes) {
			XmlElement elem = node as XmlElement;
			if (elem != null && elem.Name == tag)
				return elem;
		}
		return null;
	}
	/// <summary> Equivalent to the XElement.Elements </summary>
	public static IEnumerable<XmlElement> Es(this XmlElement self, string tag = null) {
		foreach (XmlNode node in self.ChildNodes) {
			XmlElement elem = node as XmlElement;
			if (elem == null) continue;
			if (tag == null || elem.Name == tag)
				yield return elem;
		}
	}
	/// <summary> Get the string value of the first children with that tag. </summary>
	public static string ChildValue(this XmlElement self, string tag) {
		self = self.E(tag);
		if (self == null) return null;
		return self.Value;
	}
	/// <summary> Get number value of the first children with that tag. </summary>
	public static double NumberValue(this XmlElement self, string tag, double @default = -1) {
		self = self.E(tag);
		if (self == null) return @default;
		double number;
		if (double.TryParse(self.Value, out number))
			return number;
		return @default;
	}
	#endregion

	#region Get children/child By...
	/// <summary>
	/// Searches for an element containing attribute by name
	/// "aName" and value "aValue".
	/// (Optional) In addition you can specify the tag required.
	/// The method returns null if no item found.
	/// </summary>
	/// <param name="parent">Self</param>
	/// <param name="aName">Attribute Name</param>
	/// <param name="aValue">Attribute Value</param>
	/// <param name="tag">(Optional)Required Tag</param>
	/// <returns>First Occurrence</returns>
	public static XmlElement ChildByAttribute(this XmlElement parent, string aName, string aValue, string tag) {
		var children = tag == null ? parent.Es() : parent.Es(tag);
		foreach (var child in children)
			if (child.A(aName) == aValue) return child;
		return null;
	}
	/// <summary>
	/// Searches for all elements containing attribute by name
	/// "aName" and value "aValue".
	/// (Optional) In addition you can specify the tag required.
	/// </summary>
	/// <param name="parent">Self</param>
	/// <param name="aName">Attribute Name</param>
	/// <param name="aValue">Attribute Value</param>
	/// <param name="tag">(Optional)Required Tag</param>
	/// <returns>All Occurrences</returns>
	public static IEnumerable<XmlElement> ChildrenByAttribute(this XmlElement parent, string aName, string aValue, string tag = null) {
		foreach (var child in parent.sElements(tag)) {
			if (child.A(aName) == aValue)
				yield return child;
		}
	}
	#endregion

	/// <summary> Safe Elements, if the parameter is null it gets ignored. </summary>
	public static IEnumerable<XmlElement> sElements(this XmlElement self, string tag) {
		if (tag == null) return self.Es();
		return self.Es(tag);
	}

	#endregion

	#region Get attribute
	/// <summary> Get string attribute. </summary>
	public static string A(this XmlElement self, string attribName, string @default = null) {
		return self.GetAttribute(attribName) ?? @default;
	}
	/// <summary> Get double attribute. </summary>
	public static double dA(this XmlElement self, string attribName, double @default = -1.0) {
		var attrib = self.A(attribName);
		if (attrib == null) return @default;
		return double.Parse(attrib); // let it throw if not a number
	}
	/// <summary> Get boolean attribute. </summary>
	public static bool bA(this XmlElement self, string attribName, bool @default = false) {
		var attrib = self.A(attribName);
		if (attrib == null) return @default;
		return bool.Parse(attrib);
	}
	#endregion

	#region Tweak
	/// <summary> Adjust the value of the first child element,
	/// applying basic mathematic operations. </summary>
	/// <param name="tag">Searching for the child with that tag.</param>
	/// <param name="multiply">Multiply</param>
	/// <param name="add">Add</param>
	/// <param name="round">Round, if 0 it's ignored</param>
	public static void ChildTweak(this XmlElement self, string tag, double multiply = 1.0, double add = 0, double round = 0) {
		self.E(tag).Tweak(multiply, add, round);
	}
	public static void ChildTweak(this XmlElement self, XmlElement mod) {
		ChildTweak(self, mod.A("Tag"), mod.dA("Multiply", 1.0), mod.dA("Add", 0), mod.dA("Round", 0));
	}
	/// <summary> Adjust the value of self, by applying basic mathematic operations. </summary>
	public static void Tweak(this XmlElement self, double multiply = 1, double add = 0, double round = 0) {
		double v = double.Parse(self.Value) * multiply + add;
		if (round != 0) v = (int)(v / round) * round;
		self.Value = v.ToString();
	}
	public static void Tweak(this XmlElement self, XmlElement mod) {
		Tweak(self, mod.dA("Multiply", 1.0), mod.dA("Add", 0), mod.dA("Round", 0));
	}
	/// <summary> Same us tweak but it applies to all children with that tag </summary>
	public static void ChildrenTweak(this XmlElement self, string tag,
		double multiply = 1.0, double add = 0, double round = 0) {
		foreach (var child in self.Es(tag)) {
			child.Tweak(multiply, add, round);
		}
	}
	public static void ChildrenTweak(this XmlElement self, XmlElement mod) {
		ChildrenTweak(self, mod.A("Tag"), mod.dA("Multiply", 1.0), mod.dA("Add", 0), mod.dA("Round", 0));
	}
	/// <summary> Same as tweak but it applies to the attribute by name "attribName". </summary>
	public static void TweakA(this XmlElement self, string attribName, double multiply = 1.0, double add = 0, double round = 0) {
		double v = self.dA(attribName) * multiply + add;
		if (round != 0) v = (int)(v / round + 0.5) * round;

		self.Attributes[attribName].Value = v.ToString();
	}
	public static void TweakA(this XmlElement self, XmlElement mod) {
		ChildrenTweak(self, mod.A("Name"), mod.dA("Multiply", 1.0), mod.dA("Add", 0), mod.dA("Round", 0));
	}
	#endregion
}

