namespace MCSharp.Linkage.Minecraft.Text;

public class ScoreData {

	/// <summary>The name of the score holder whose score should be displayed. This can be a selector like @p or an explicit name.
	/// If the text is a selector, the selector must be guaranteed to never select more than one entity, possibly by adding limit=1.
	/// If the text is "*", it shows the reader's own score (for example, /tellraw @a {"score":{"name":"*","objective":"obj"}} shows every online player their own score in the "obj" objective).</summary>
	public string Name { get; set; }
	/// <summary>The internal name of the objective to display the player's score in.</summary>
	public string Objective { get; set; }
	/// <summary>Optional. If present, this value is used regardless of what the score would have been.</summary>
	public string Value { get; set; }

}
