using System.Text;

namespace FightWasteConsole.Output
{
    public class ModelTableOutputter<T> : IModelCollectionOutputter<T> where T : class
    {
        public string GetListAsCollection(List<T> models)
        {
            var cells = new List<List<string>>();

            var headers = models.FirstOrDefault()
                .GetType()
                .GetProperties()
                .Select(prop => prop.Name)
                .ToList();

            headers.ForEach(header =>
            {
                cells.Add(new());
                cells.ElementAt(headers.IndexOf(header)).Add(header);
            });

            models.ForEach(model =>
            {
                var properties = model.GetType().GetProperties().ToList();
                properties.ForEach(prop =>
                {
                    cells.ElementAt(properties.IndexOf(prop)).Add(prop.GetValue(model).ToString());
                });
            });

            var columnWidths = cells.Select(column => column.Select(cell => cell.Length).Max()).ToList();

            cells.ForEach(column =>
            {
                var stringBuilder = new StringBuilder();

                for (int i = 0; i < column.Select(item => item.Length).Max();  i++)
                {
                    stringBuilder.Append('-');
                }

                column.Insert(1, stringBuilder.ToString());
            });

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < cells.First().Count; i++)
            {
                var innerStringBuilder = new StringBuilder();
                innerStringBuilder.Append('|');

                for (int j = 0; j < cells.Count; j++)
                {
                    innerStringBuilder.Append($" {cells[j][i].PadRight(columnWidths[j])} |");
                }

                stringBuilder.AppendLine(innerStringBuilder.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
