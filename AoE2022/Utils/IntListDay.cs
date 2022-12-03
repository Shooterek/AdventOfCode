namespace AoE2022.Utils;

public abstract class IntListDay : Day {
    public List<int> Input { get; private set; }
    public override void LoadInput() {
        this.Input = this._inputLoader.LoadIntListInput();
    }
}