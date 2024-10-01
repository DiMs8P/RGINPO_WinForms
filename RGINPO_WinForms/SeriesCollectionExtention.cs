using RGINPO_WinForms;
using System.Collections.ObjectModel;

public static class SeriesCollectionExtensions
{
    public static Series2D FindByName(this ObservableCollection<Series2D> collection, string name)
    {
        return collection.FirstOrDefault(series => series.Name == name)!;
    }
}