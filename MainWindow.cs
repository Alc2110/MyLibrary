using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibraryWinForms.Model.ObjectModel;
using MyLibraryWinForms.LogicLayer;
using MyLibraryWinForms.LogicLayer.Specifications;

namespace MyLibraryWinForms
{
    public partial class MainWindow : Form
    {
        private List<string> itemTypes = new List<string>() { "Books", "Media Items (all types)" };

        public MainWindow()
        {
            InitializeComponent();

            // complete the list of item types
            foreach (var t in Enum.GetValues(typeof(MediaType)))
                itemTypes.Add(t.ToString() + "s"); // pluralise

            // populate the items type combo box
            itemTypesComboBox.DataSource = itemTypes;
            itemTypesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // select the "Books" option in the items type combo box by default
            itemTypesComboBox.SelectedIndex = 0;

            itemsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            itemsGrid.AllowUserToAddRows = false;

            PopulateMainList();

            // TODO: subscribe to required events

        }

        private void PopulateMainList()
        {
            // first clear the data grid view
            itemsGrid.DataSource = null;

            // create a data table
            var itemsTable = new DataTable();
            itemsTable.Columns.Clear();
            itemsTable.Columns.Add("Id"); // this field will be invisible
            string selectedType = itemTypesComboBox.Text;
            if (itemTypes.Contains(selectedType))
            {
                var repo = new ItemRepository();

                if (itemTypesComboBox.Text == "Books")
                {
                    // display books
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("ISBN");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Authors");
                    itemsTable.Columns.Add("Publisher");
                    itemsTable.Columns.Add("Date Published");
                    itemsTable.Columns.Add("Pages");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var book in repo.GetBooks())
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = book.id;
                        newRow["ISBN"] = book.getIsbn();
                        newRow["Title"] = book.titleLong;
                        newRow["Authors"] = GetAuthorsToDisplay(book.authors);
                        newRow["Publisher"] = book.publisher.name;
                        newRow["Date Published"] = book.datePublished;
                        newRow["Pages"] = book.pages;
                        newRow["Tags"] = book.getCommaDelimitedTags();
                        newRow["Notes"] = book.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "Media Items (all types)")
                {
                    // display media items
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems())
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "Dvds")
                {
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems(new IsDvd()))
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "BluRays")
                {
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems(new IsBluray()))
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "Cds")
                {
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems(new IsCd()))
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "Vhss")
                {
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems(new IsVhs()))
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "Vinyls")
                {
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems(new IsVinyl()))
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else if (itemTypesComboBox.Text == "Others")
                {
                    itemsTable.TableName = selectedType;
                    itemsTable.Columns.Add("Number");
                    itemsTable.Columns.Add("Type");
                    itemsTable.Columns.Add("Title");
                    itemsTable.Columns.Add("Running Time");
                    itemsTable.Columns.Add("Release Year");
                    itemsTable.Columns.Add("Tags");
                    itemsTable.Columns.Add("Notes");
                    foreach (var item in repo.GetMediaItems(new IsOther()))
                    {
                        DataRow newRow = itemsTable.NewRow();
                        newRow["Id"] = item.id;
                        newRow["Number"] = item.number;
                        newRow["Type"] = item.type;
                        newRow["Title"] = item.title;
                        newRow["Running Time"] = item.runningTime;
                        newRow["Release Year"] = item.releaseYear;
                        newRow["Tags"] = item.getCommaDelimitedTags();
                        newRow["Notes"] = item.notes;
                        itemsTable.Rows.Add(newRow);
                    }
                }
                else
                {
                    // something else
                    // should never happen
                    // show books by default
                    itemTypesComboBox.SelectedIndex = 0;
                    PopulateMainList();
                }

                // bind the data table to the grid
                // TODO: rows are invisible by default
                itemsGrid.DataSource = itemsTable;

                // TODO: apply filters
                FilterMainList();
            }
        }

        private string GetAuthorsToDisplay(IEnumerable<Author> authors)
        {
            var authorsBuilder = new StringBuilder();
            int count = 1;
            foreach (var a in authors)
            { 
                authorsBuilder.Append(a.getFullNameLastNameCommaFirstName());
                if (count < authors.Count())
                    authorsBuilder.Append("; ");

                count++;
            }

            return authorsBuilder.ToString();
        }

        private void FilterMainList()
        {
            itemsGrid.Columns[0].Visible = false; // the first column is the DB id column, make it invisible
            // TODO: filtering
        }

        #region UI event handlers
        private void itemTypesComboBox_TextChanged(object sender, EventArgs e)
        {
            PopulateMainList();
        }
        #endregion
    }
}