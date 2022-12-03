namespace AoE2022.Utils;

public abstract class EntireStringDay : Day {
    public string Input { get; private set; }
    public override void LoadInput() {
        this.Input = this._inputLoader.LoadEntireString();
    }
}