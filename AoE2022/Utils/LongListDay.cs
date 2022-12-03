namespace AoE2022.Utils;

public abstract class LongListDay : Day {
    public List<long> Input { get; private set; }
    public override void LoadInput() {
        this.Input = this._inputLoader.LoadLongListInput();
    }
}