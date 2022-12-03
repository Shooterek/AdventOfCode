namespace AoE2022.Utils;

public abstract class IntListFromLineDay : Day {
    public List<int> Input { get; private set; }

    public virtual string Separator { private get; set; } = ",";
    public override void LoadInput() {
        this.Input = this._inputLoader.LoadIntListFromOneLine(this.Separator);
    }
}