using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using System.Data;

namespace SSOM
{
    class Program
    {
        static void Main(string[] args)
        {
            //_GetWebProperties();
            //_SetWebProperties();
            //_GetLists();
           // _GetFields();
            _AddField();
           // GetListItems();
           // GetListItem();
           // CreateListItem();
           // UpdateListItem();
           // DeleteListItem();
           // FilterListItem();
           // FilterByFolderListItem();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
        static String _Url = "http://iqv2:1010/";
        static void _GetWebProperties()
        {
            //SPSite _Site = new SPSite(SPContext.Current.Web.Url);
            SPSite _Site = new SPSite(_Url);
            SPWeb _Web = _Site.OpenWeb();
            Console.WriteLine(_Web.Title);
            _Web.Dispose();
            _Site.Dispose();
        }
        static void _SetWebProperties()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    //for (int _Count = 0; _Count < 1000; _Count++)
                    {
                        // _Web.Webs.Add("SbSite","ABC",,"",1033,);
                    }
                    //_Web.AllowUnsafeUpdates = true;
                    //_Web.Title = "SharePointIQ | Noida";
                    //_Web.Description = "This site will be used for Noida Location.";
                    //_Web.Update();
                    //_Web.AllowUnsafeUpdates = false;
                }
            }
        }
        static void _GetLists()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    SPListCollection _Lists = _Web.Lists;
                    foreach (SPList _List in _Lists)
                       // if (_List.BaseTemplate==SPListTemplateType.)
                        {
                            Console.WriteLine(_List.Title);
                        }
                }
            }
        }
        static void _GetFields()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    SPList _List = _Web.Lists["Employee"];

                    SPFieldCollection _Fields = _List.Fields;
                    foreach (SPField _Field in _Fields)
                    {
                        Console.WriteLine(_Field);

                        //  Console.WriteLine(_Field.TitleResource);

                    }
                }
            }
        }
        static void _AddField()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    SPList _List = _Web.Lists["Employee"];
                    _List.Fields.Add("WorkPhone", SPFieldType.Text, true);
                }
            }
        }
       
        static void GetListItems()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];
                   // _List.Views
                    SPListItemCollection _Items = _List.GetItems();
                   // System.Data.DataTable _tb = _Items.GetDataTable();
                    //SPListItemCollection _Items = _List.GetItems("ID", "Title", "SL");
                   // System.Data.DataTable _Table = _List.GetItems("ID", "Title", "SL").GetDataTable();
                    foreach (SPListItem _Item in _Items)
                    {
                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    }


                }
            }
        }
        static void GetListItem()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];
                    SPListItem _Item = _List.GetItemById(1);

                    Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    //TextBoxTitle.Text = Convert.ToString(_Item["Title"]);
                }
            }
        }
        static void CreateListItem()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    _Web.AllowUnsafeUpdates = true;
                    SPList _List = _Web.Lists["Leave"];
                    SPListItem _Item = _List.AddItem();
                    //SPListItem _Item = _List.Items.Add();
                    _Item["Title"] = "EMP007";
                    _Item["SL"] = 15;
                    _Item.Update();
                   // _Item.SystemUpdate();
                    _Web.AllowUnsafeUpdates = false;
                    Console.WriteLine("Created");
                }
            }
        }
        static void UpdateListItem()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    _Web.AllowUnsafeUpdates = true;
                    SPList _List = _Web.Lists["Leave"];
                    //SPListItem _Item = _List.AddItem();
                    SPListItem _Item = _List.GetItemById(3);
                    // _Item["Title"] = "EMP007";
                    _Item["SL"] = 25;
                    _Item.Update();
                    //  _Item.SystemUpdate(false);
                    _Web.AllowUnsafeUpdates = false;
                    Console.WriteLine("Updated");
                }
            }
        }
        static void DeleteListItem()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    _Web.AllowUnsafeUpdates = true;
                    SPList _List = _Web.Lists["Leave"];
                    //SPListItem _Item = _List.AddItem();
                    SPListItem _Item = _List.GetItemById(3);
                    // _Item["Title"] = "EMP007";
                    _Item.Delete();
                    //  _Item.SystemUpdate(false);
                    _Web.AllowUnsafeUpdates = false;
                    Console.WriteLine("Deleted");
                }
            }
        }
        static void _AddField()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    SPList _List = _Web.Lists["Employee"];
                    _List.Fields.Add("WorkPhone", SPFieldType.Text, true);
                }
            }
        }
        static void FilterListItem()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];
                    SPQuery _Query = new SPQuery();
                    _Query.Query = @" <Where>
                                      <Eq>
                                          <FieldRef Name='Title' />
                                           <Value Type='Text'>EMP001</Value>
                                      </Eq>
                                   </Where>";
                    _Query.RowLimit = 100;

                    _Query.ViewFields = " <FieldRef Name='Title' /> <FieldRef Name='ID' />";
                    SPListItemCollection _Items = _List.GetItems(_Query);
                    //SPListItemCollection _Items = _List.GetItems();
                    // System.Data.DataTable _Table = _List.GetItems(_Query).GetDataTable();
                    foreach (SPListItem _Item in _Items)
                    {
                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    }

                }
            }
        }
//        static void FilterListItem()
//        {
//            using (SPSite _Site = new SPSite("http://spiq:2020"))
//            {
//                using (SPWeb _Web = _Site.OpenWeb())
//                {

//                    SPList _List = _Web.Lists["Leave"];
//                    SPQuery _Query = new SPQuery();
//                    _Query.Query = @" <Where>
//                                      <In>
//                                         <FieldRef Name='ID' />
//                                         <Values>
//                                            <Value Type='Counter'>1</Value> 
//                                            <Value Type='Counter'>2</Value>
//                                         <Values>
//                                      </In>
//                                   </Where>";


//                    //                    _Query.Query = @"<Where>
//                    //                                      <And>"+
//                    //                          (  (true)  ?          @" <Eq>
//                    //                                            <FieldRef Name='ID' />
//                    //                                            <Value Type='Counter'>1</Value>
//                    //                                         </Eq>": "<IsNotNull><FieldRef Name='ID' /></IsNotNull>")+
//                    //                                        @"<And>
//                    //                                            <Neq>
//                    //                                               <FieldRef Name='h2mi' />
//                    //                                               <Value Type='Text'>0</Value>
//                    //                                            </Neq>
//                    //                                            <And>
//                    //                                               <IsNotNull>
//                    //                                                  <FieldRef Name='ID' />
//                    //                                               </IsNotNull>
//                    //                                               <Contains>
//                    //                                                  <FieldRef Name='ID' />
//                    //                                                  <Value Type='Counter'>0</Value>
//                    //                                               </Contains>
//                    //                                            </And>
//                    //                                         </And>
//                    //                                      </And>
//                    //                                   </Where>";
//                    _Query.RowLimit = 100;

//                    _Query.ViewFields = " <FieldRef Name='Title' /> <FieldRef Name='ID' />";
//                    SPListItemCollection _Items = _List.GetItems(_Query);
//                    // System.Data.DataTable _Table = _List.GetItems(_Query).GetDataTable();
//                    foreach (SPListItem _Item in _Items)
//                    {
//                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
//                    }

//                }
//            }
//        }
        static void FilterByFolderListItem()
        {
            using (SPSite _Site = new SPSite(_Url))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];
                    SPFolder oFolder = _List.RootFolder.SubFolders["IT"];
                    SPQuery _Query = new SPQuery();
                    _Query.Query = @" <Where>
                                      <Eq>
                                          <FieldRef Name='Title' />
                                           <Value Type='Text'>EMP004</Value>
                                      </Eq>
                                   </Where>";
                    _Query.RowLimit = 100;
                    _Query.Folder = oFolder;
                    _Query.ViewFields = " <FieldRef Name='Title' /> <FieldRef Name='ID' />";
                    SPListItemCollection _Items = _List.GetItems(_Query);
                    // System.Data.DataTable _Table = _List.GetItems(_Query).GetDataTable();
                    foreach (SPListItem _Item in _Items)
                    {
                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    }

                }
            }
        }
        static void FilterByFileonlyListItem()
        {
            using (SPSite _Site = new SPSite(_Url))            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];
                    SPFolder oFolder = _List.RootFolder.SubFolders["Folder_Name"];
                    SPQuery _Query = new SPQuery();
                    _Query.Query = @" <Where>
                                      <Eq>
                                         <FieldRef Name='ID' />
                                         <Value Type='Counter'>1</Value>
                                      </Eq>
                                   </Where>";
                    _Query.RowLimit = 100;
                    _Query.Folder = oFolder;
                    _Query.ViewFields = " <FieldRef Name='Title' /> <FieldRef Name='ID' />";
                    _Query.ViewAttributes = "Scope=\"FilesOnly\"";
                    SPListItemCollection _Items = _List.GetItems(_Query);
                    // System.Data.DataTable _Table = _List.GetItems(_Query).GetDataTable();
                    foreach (SPListItem _Item in _Items)
                    {
                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    }

                }
            }
        }
        static void FilterByRecursiveListItem()
        {
            using (SPSite _Site = new SPSite("http://spiq:2020"))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];
                    SPFolder oFolder = _List.RootFolder.SubFolders["Folder_Name"];
                    SPQuery _Query = new SPQuery();
                    _Query.Query = @" <Where>
                                      <Eq>
                                         <FieldRef Name='ID' />
                                         <Value Type='Counter'>1</Value>
                                      </Eq>
                                   </Where>";
                    _Query.RowLimit = 100;
                    _Query.Folder = oFolder;
                    _Query.ViewFields = " <FieldRef Name='Title' /> <FieldRef Name='ID' />";
                    //Using Recursive attribute - Show all files and all subfolders of all folders.
                    _Query.ViewAttributes = "Scope=\"Recursive\"";
                    SPListItemCollection _Items = _List.GetItems(_Query);
                    // System.Data.DataTable _Table = _List.GetItems(_Query).GetDataTable();
                    foreach (SPListItem _Item in _Items)
                    {
                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    }

                }
            }
        }
        static void FilterByRecursiveAllListItem()
        {
            using (SPSite _Site = new SPSite("http://spiq:2020"))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {

                    SPList _List = _Web.Lists["Leave"];

                    SPQuery _Query = new SPQuery();
                    _Query.Query = @" <Where>
                                      <Eq>
                                         <FieldRef Name='ID' />
                                         <Value Type='Counter'>1</Value>
                                      </Eq>
                                   </Where>";
                    _Query.RowLimit = 100;

                    _Query.ViewFields = " <FieldRef Name='Title' /> <FieldRef Name='ID' />";
                    //Using Recursive attribute - Show all files and all subfolders of all folders.
                    _Query.ViewAttributes = "Scope=\"RecursiveAll\"";
                    _Query.ViewAttributes = "Scope='Recursive' ModerationType='HideUnapproved'";
                    //HideUnapproved -- Unapproved draft items are hidden from users who only have permission to read items.
                    //Contributor -- Pending and rejected items for the current user are displayed.
                    //Moderator -- Pending and rejected items for all users are displayed to users who have managed list permissions.
                    SPListItemCollection _Items = _List.GetItems(_Query);
                    // System.Data.DataTable _Table = _List.GetItems(_Query).GetDataTable();
                    foreach (SPListItem _Item in _Items)
                    {
                        Console.WriteLine(_Item.ID + " - " + Convert.ToString(_Item["Title"]));
                    }

                }
            }
        }
        //LINQ
        //C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\BIN> spmetal.exe /web:http://sharepointiq:7070/ /namespace:nwind /code:Product.cs
        //static void Main(string[] args)
        //{
        //    ProductDataContext context = new ProductDataContext("http://sharepointiq:7070/");
        //    //EntityList<Test1_ProductItem> products = context.GetList<Test1_ProductItem>("Test1_Product");
        //    var res = from r in context.Test1_Product select r;
        //    foreach (var r in res)
        //    {
        //        Console.WriteLine(r.ProductId + ":" + r.ProductName + ":" + r.ProductPrice);
        //    }
        //    Console.ReadKey(true);
        //}

        static void FilterJoinListItem()
        {
            using (SPSite site = new SPSite("http://sharepointiq:7070/"))
            {
                SPWeb web = site.RootWeb;

                SPQuery query = new SPQuery();

                query.Joins = "<Join Type='INNER' ListAlias='DSE'>" +
                                "<Eq>" +
                                    "<FieldRef Name='Manager' RefType='Id'/>" +
                                    "<FieldRef List='DSE' Name='ID'/>" +
                                "</Eq>" +
                                "</Join>";
                query.ProjectedFields =
                    "<Field Name='DSEFirstName' Type='Lookup' " +
                            "List='DSE' ShowField='FirstName'/>" +
                    "<Field Name='DSELastName' Type='Lookup' " +
                            "List='DSE' ShowField='Title'/>";

                query.ViewFields = "<FieldRef Name='Title'/>" +
                                    "<FieldRef Name='DSEFirstName'/>" +
                                    "<FieldRef Name='DSELastName'/>";
                SPList customerList = web.Lists["Projects"];
                SPListItemCollection items = customerList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    SPFieldLookupValue dseLastName =
                        new SPFieldLookupValue(item["DSELastName"].ToString());
                    SPFieldLookupValue dseFirstName =
                        new SPFieldLookupValue(item["DSEFirstName"].ToString());

                    Console.WriteLine("{0}  {1}   {2}",
                            item.Title,
                            dseLastName.LookupValue,
                            dseFirstName.LookupValue);
                }
            }
        }
        static void FilterSiteData()
        {
            using (SPSite _Site = new SPSite("http://spiq:2020"))
            {
                using (SPWeb _Web = _Site.OpenWeb())
                {
                    SPSiteDataQuery query = new SPSiteDataQuery();

                    // The Announcements list type
                    query.Lists = @"<Lists ServerTemplate=""104"" />";
                    query.ViewFields = @"<FieldRef Name=""Title""/><FieldRef Name=""Expires""/><FieldRef Name=""Created""/>";
                    query.Webs = "<Webs Scope='Recursive' />";
                    string queryText =
                    @"<Where>
                        <Lt>
                          <FieldRef Name=""Created"" />
                          <Value Type=""DateTime"">
                            <Today />
                          </Value>
                        </Lt>
                        </Where>";

                    query.Query = queryText;

                    DataTable table = _Web.GetSiteData(query);

                    foreach (DataColumn col in table.Columns)
                    {
                        Console.WriteLine(col.ColumnName);
                        Console.WriteLine(col.DataType);
                    }
                    foreach (DataRow row in table.Rows)
                    {

                        // If you needed to get an SPListItem reference... 
                        SPWeb parentWeb = _Web.Site.OpenWeb(new Guid(row["WebId"].ToString()));
                        SPList list = parentWeb.Lists[new Guid(row["ListId"].ToString())];
                        SPListItem item = list.GetItemById((int.Parse(row["ID"].ToString())));
                        Console.WriteLine(item.Title);

                        Console.WriteLine(row["Title"].ToString());
                    }
                }
            }
        }
        static void _Filter()
        {
            SPSite _site = new SPSite("http://sharepointiq:7070/");
            SPWeb _web = _site.OpenWeb();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite("http://sharepointiq:7070/"))
                {
                    using (SPWeb web = site.OpenWeb())
                    {

                        SPList _List = _web.Lists["Projects"];
                        //SPListItemCollection _SPListItemCollection = _List.Items;
                        // SPListItem _NewItem = _List.GetItemById (1001);
                        SPQuery _SPQuery = new SPQuery();
                        _SPQuery.Query = @" <Where>  <Eq>
               <FieldRef Name='ID' />
               <Value Type='Text'>1001</Value>
            </Eq></Where>";
                        // SPListItem _NewItem1 = _List.Items.Add();
                        SPListItemCollection _Col = _List.GetItems(_SPQuery);
                        foreach (SPListItem _Item in _Col)
                        {
                            SPListItem _NewItem = _List.GetItemById(_Item.ID);
                            _NewItem["Title"] = "PRO2001";
                            _NewItem["Description"] = "PROject PMS";
                            _NewItem.Update();
                            // _NewItem.SystemUpdate();
                            _NewItem.SystemUpdate(false);
                            // web.AllowUnsafeUpdates = false;
                            //SPListItem _NewItem = _List.GetItemById(8);
                            //_NewItem["Title"] = "Shambhu Singh";
                            //_NewItem.Delete();
                        }
                    }
                }
                Console.ReadLine();
            });
        }
    }

}
