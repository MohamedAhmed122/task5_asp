using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Globalization;
using SAP.ViewModels.Catalogue.Admin;
using SAP.ViewModels.Catalogue;


namespace SAP.Services.Catalogue.Implementations
{
    class TXTReader : IFileReader
    {
        public List<CreateItemViewModel> UpdateCatalogue(String filepath)
        {
            int counter = 0;
            string line;
            var itemList = new List<CreateItemViewModel>();

            System.IO.StreamReader file =
                new System.IO.StreamReader(filepath);
            while ((line = file.ReadLine()) != null)
            {
                if (counter > 0)
                {
                    string attributes = line.Substring(line.IndexOf("["), line.IndexOf("]") + 1 - line.IndexOf("["));
                    var formattedLine = line.Replace(";" + attributes, string.Empty);
                    attributes = attributes.Trim(new Char[] { '[', ']' });
                    List<string> tempList = formattedLine.Split(';').ToList<string>();
                    CreateItemViewModel tempItem = new CreateItemViewModel();
                    while (tempList.Any())
                    {
                        tempItem.Name = tempList[0];
                        tempItem.CategoryId = Guid.Parse(tempList[1]);
                        tempItem.ManufacturerId = tempList[2];
                        tempItem.QuantityInStock = int.Parse(tempList[3]);
                        tempItem.PictureUrl = tempList[4];
                        List<string> attributesList = attributes.Split(';').ToList<string>();
                        tempItem.Attributes = new List<AttributesViewModel>(attributesList.Count);
                        foreach (string str in attributesList)
                        {
                            List<string> attributesSpecsList = str.Split(':').ToList<string>();
                            AttributesViewModel attributeTemp = new AttributesViewModel();
                            attributeTemp.Color = (Data.Models.Catalogue.Colors)int.Parse(attributesSpecsList[0]);
                            attributeTemp.Price = decimal.Parse(attributesSpecsList[1], NumberStyles.Currency);
                            tempItem.Attributes.Add(attributeTemp);
                        }
                        tempList.RemoveRange(0, 5);
                    }

                    itemList.Add(tempItem);
                }
                counter++;
            }
            file.Close();
            return itemList;
        }
    }
}
