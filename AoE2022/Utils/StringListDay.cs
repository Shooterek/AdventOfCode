namespace AoE2022.Utils;

public abstract class StringListDay : Day {
    public List<string> Input { get; private set; }
    public override void LoadInput() {
        this.Input = this._inputLoader.LoadStringListInput();
    }
}