namespace UI
{
    public interface IViewable<in T> where T : IData
    {
        void Show();
        void Hide();
        void UpdateView(T taskData);
    }
    
    public interface IData { }
}