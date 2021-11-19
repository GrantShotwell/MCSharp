namespace MCSharp.Linkage.Minecraft.Text;

public class HoverContents {

	/// <summary>Another raw JSON text component. Can be any valid text component type: string, array, or object.
	/// Note that clickEvent and hoverEvent do not function within the tooltip.</summary>
	public object ShowText { get; set; }
	/// <summary>The item that should be displayed.</summary>
	public HoverContentItem ShowItem { get; set; }
	/// <summary>The entity that should be displayed.</summary>
	public HoverContentEntity ShowEntity { get; set; }

}
