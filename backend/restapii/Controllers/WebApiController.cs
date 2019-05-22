using restapii.Context;
using restapii.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace restapii.Controllers
{
    //public class tokens
    //{
    //    //-- bruid
    //    public string bruid { get; set; }
    //    //-- t ->> token
    //    public string t { get; set; }
    //    //-- d ->> domain
    //    public string d { get; set; }
    //} 
    public class WebApiController : ApiController
    {
        [HttpGet]
        [Route("~/api/products")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> productList()
        {
 
            RestApiiContext db = new RestApiiContext();
            string sql = @"SELECT * FROM tblAsset ast WHERE ISNULL(deleted,0)=0";
            IEnumerable<tblAsset> assets = db.Database.SqlQuery<tblAsset>(sql).ToList();

            return Json(new { assets });

        }

        /*
        [Route("~/api/products")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> productList()
        {
            string email = "", userName = "", storeName = "";
            int sellerId = 0, basketCount = 0;
            RestApiiContext db = new RestApiiContext();
            IEnumerable<myProduct> products = Enumerable.Empty<myProduct>();
            try
            {
                var bodyData = await Request.Content.ReadAsStringAsync();
                //-- SINGLE json records
                tokens objTokens = (tokens)Newtonsoft.Json.JsonConvert.DeserializeObject(bodyData, typeof(tokens));
                SetSession("browserId", objTokens.bruid);
                SetSession("token", objTokens.t);
                SetSession("domain", objTokens.d);
                string domain = objTokens.d + "";
                if (domain.Contains("localhost:8080")) { domain = "babynmom.me"; }
                else if (domain.Contains("localhost")) { domain = ""; }

                email = GetSession("email") + ""; userName = GetSession("userName") + ""; storeName = GetSession("storeName") + "";
                sellerId = Convert.ToInt32("0" + GetSession("sellerId"));

              
                basketCount = GetBasketCount();
 
                string sql =
                    @"SELECT prd.productId, prd.sellerId,prd.sellerName, prd.categoryId,prd.categoryName,prd.categoryCode,prd.productName,
                            prd.brandName,prd.color,prd.imageUrlMain,prd.gender,prd.likes,prd.catLevel1,prd.catLevel2,prd.currencyCode,
                            prd.altPrice,prd.size,prd.condition
                    FROM tblProduct prd ORDER BY categoryCode, brandName";


                products = db.Database.SqlQuery<myProduct>(sql).ToList();

            }
            catch (Exception ex) {
                string test = "";
            }
            finally { db.Dispose(); }
            return Json(new { email, userName, storeName, basketCount, products });

        }
       //public void sessionGetSet()
        //{

        //}


        //[Route("~/api/item/basketCount/")]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public int GetBasketCount()
        {
            int customerId = Convert.ToInt32("0" + GetSession("customerId"));
            string browserId = GetSession("browserId") + "";

            RestApiiContext db = new RestApiiContext();
            int basketCount = db.Baskets.Where(i => i.deleted != true && i.quantity > 0 &&
                ((i.customerId == 0 && i.browserId == browserId) || (i.customerId > 0 && i.customerId == customerId))
                ).Select(s => s.quantity).ToList().Sum();
            //int basketCount = db.Baskets.Where(i => i.quantity > 0 && i.customerId == customerId && i.browserId == browserId).Select(s => s.quantity).ToList().Sum();

            return basketCount;
        }

        [Route("~/api/products/{search}/s/")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> productSearch(string search = "")
        {
            string email = "", userName = "", storeName = "";
            int sellerId = 0, basketCount = 0;
            string keywords = search.Replace("-", " ");
            int sortBy = 0, page = 1, rows = 100000;
            RestApiiContext db = new RestApiiContext();
            IEnumerable<myProduct> products = Enumerable.Empty<myProduct>();
            try
            {
                var bodyData = await Request.Content.ReadAsStringAsync();
                //-- SINGLE json records
                tokens objTokens = (tokens)Newtonsoft.Json.JsonConvert.DeserializeObject(bodyData, typeof(tokens));
                SetSession("browserId", objTokens.bruid);
                SetSession("token", objTokens.t);
                SetSession("domain", objTokens.d);
                string domain = objTokens.d + "";
                if (domain.Contains("localhost:8080")) { domain = "babynmom.me"; }
                else if (domain.Contains("localhost")) { domain = ""; }

                email = GetSession("email") + ""; userName = GetSession("userName") + ""; storeName = GetSession("storeName") + "";
                sellerId = Convert.ToInt32("0" + GetSession("sellerId"));

                //ToolsController tools = new ToolsController();
                //string _TgE_72a11t30 = tools.getToken();
                //tools.Dispose();

                basketCount = GetBasketCount();

                products = db.Database.SqlQuery<myProduct>
                    ("sp_GetProductList2 @Domain, @Keywords",
                        new SqlParameter("Domain", domain),
                        new SqlParameter("Keywords", keywords),
                        new SqlParameter("SortBy", sortBy),
                        new SqlParameter("Page", page),
                        new SqlParameter("Rows", rows)
                    ).ToList();


                sellerId = Convert.ToInt32("0" + GetSession("sellerId"));
                if (sellerId > 0)
                {
                    products = products.Where(i => i.sellerId == sellerId).ToList();
                }
            }
            catch (Exception ex)
            {
                string test = "";
            }
            finally { db.Dispose(); }
            return Json(new { email, userName, storeName, basketCount, products });
        }

        [Route("~/api/products/{item}/d/")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> productDetails(string item = "")
        {
            string email = "", userName = "", storeName = "";
            int sellerId = 0, basketCount = 0;
            RestApiiContext db = new RestApiiContext();
            IEnumerable<modelProduct> products = Enumerable.Empty<modelProduct>();
            try
            {
                var bodyData = await Request.Content.ReadAsStringAsync();
                //-- SINGLE json records
                tokens objTokens = (tokens)Newtonsoft.Json.JsonConvert.DeserializeObject(bodyData, typeof(tokens));
                SetSession("browserId", objTokens.bruid);
                SetSession("token", objTokens.t);
                SetSession("domain", objTokens.d);
                string domain = objTokens.d + "";
                if (domain.Contains("localhost:8080")) { domain = "babynmom.me"; }
                else if (domain.Contains("localhost")) { domain = ""; }

                email = GetSession("email") + ""; userName = GetSession("userName") + ""; storeName = GetSession("storeName") + "";
                sellerId = Convert.ToInt32("0" + GetSession("sellerId"));
                basketCount = GetBasketCount();


                int productId = Convert.ToInt32("0" + item.Split('-').Last());

                string skuParent = "";
                ///string skuParent = db.Products.Where(c => c.productId == productId).Select(s => s.skuParent).SingleOrDefault().ToString();
                tblProduct modelProduct = db.Products.Where(c => c.productId == productId).SingleOrDefault();
                if (modelProduct != null) { skuParent = modelProduct.skuParent; }

                ///IEnumerable<tblProduct> products = db.Products.Where(c => c.skuParent == skuParent);
                products = db.Database.SqlQuery<modelProduct>(
                   @"SELECT DISTINCT ISNULL(bsk.quantity,0) quantity,ISNULL(fav.quantity,0) favorited, prod.* 
                FROM tblProduct prod 
                LEFT JOIN tblBasket bsk ON bsk.productId =prod.productId
                LEFT JOIN tblFavorite fav on fav.productId=prod.productId
                WHERE skuParent='" + skuParent + "'").ToList();


                //            IEnumerable<modelProduct> test = db.Database.SqlQuery<modelProduct>(
                //             @"SELECT prod.*, ISNULL(bsk.quantity,0) quantity FROM tblProduct prod 
                //                LEFT JOIN tblBasket bsk ON bsk.productId =prod.productId
                //                WHERE skuParent='" + skuParent + "'").ToList();


                //int customerId = gblVariables.customerId;
                //var browserId = gblVariables.browserId;
                int customerId = Convert.ToInt32("0" + GetSession("customerId"));
                string browserId = GetSession("browserId") + "";

                ////-- save viewed items
                tblViewedItem tViewedItem = db.ViewedItems.Where(i => i.customerId == customerId && i.browserId == browserId && i.productId == productId).FirstOrDefault();
                if (tViewedItem == null)
                {
                    tViewedItem = new tblViewedItem();
                    tViewedItem.customerId = customerId;
                    tViewedItem.browserId = browserId;
                    tViewedItem.productId = productId;


                    if (modelProduct != null)
                    {
                        tViewedItem.productName = modelProduct.productName;
                        tViewedItem.brandName = modelProduct.brandName;
                        tViewedItem.color = modelProduct.color;
                        tViewedItem.size = modelProduct.size;
                        tViewedItem.currencyCode = modelProduct.currencyCode;
                        tViewedItem.unitPrice = modelProduct.unitPrice;
                        tViewedItem.altPrice = modelProduct.altPrice;
                        tViewedItem.imageUrlMain = modelProduct.imageUrlMain;
                    }
                    tViewedItem.dateCreated = DateTime.Now;
                    tViewedItem.lastUpdated = DateTime.Now;
                    db.ViewedItems.Add(tViewedItem);
                }
                else
                {
                    tViewedItem.lastUpdated = DateTime.Now;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string test = "";
            }
            finally { db.Dispose(); }
            return Json(new { email, userName, storeName, basketCount, products });
        }


        //http://localhost:31000/api/products/baby-n-mom/v/
        [Route("~/api/products/{seller}/v/")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetVendor(string seller = "")
        {
            string email = "", userName = "", storeName = "";
            int basketCount = 0;
            RestApiiContext db = new RestApiiContext();
            IEnumerable<myProduct> products = Enumerable.Empty<myProduct>();
            try
            {
                var bodyData = await Request.Content.ReadAsStringAsync();
                //-- SINGLE json records
                tokens objTokens = (tokens)Newtonsoft.Json.JsonConvert.DeserializeObject(bodyData, typeof(tokens));
                SetSession("browserId", objTokens.bruid);
                SetSession("token", objTokens.t);
                SetSession("domain", objTokens.d);
                string domain = objTokens.d + "";
                if (domain.Contains("localhost:8080")) { domain = "babynmom.me"; }
                else if (domain.Contains("localhost")) { domain = ""; }

                string sql =
                     @"SELECT prd.productId, prd.sellerId,prd.sellerName, prd.categoryId,prd.categoryName,prd.categoryCode,prd.productName,
                            prd.brandName,prd.color,prd.imageUrlMain,prd.gender,prd.likes,prd.catLevel1,prd.catLevel2,prd.currencyCode,
                            prd.altPrice,prd.size,prd.condition
                    FROM (
	                        SELECT MIN(productId) productId, productName, brandName, color
	                        FROM [dbo].[vw_ProductList]
	                        GROUP BY productName, brandName, color
                        ) prod
                        LEFT JOIN tblProduct prd ON prd.productId=prod.productId " +
                            " WHERE replace(replace(rtrim(sellerName),' ','-'),'''','-')='" + seller + "'" +
                             " ORDER BY categoryCode, brandName";

                products = db.Database.SqlQuery<myProduct>(sql).ToList();

                email = "";
                userName = "";
                storeName = seller;
                basketCount = GetBasketCount();
            }
            catch (Exception ex)
            {
                string test = "";
            }
            finally { db.Dispose(); }
            return Json(new { email, userName, storeName, basketCount, products });
        }

 
        [Route("~/api/item/viewed/")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetViewed()
        {
            RestApiiContext db = new RestApiiContext();

            string browserId = GetSession("browserId") + "";

            IEnumerable<mdlViewedItem> viewed = db.Database.SqlQuery<mdlViewedItem>(
                @"SELECT TOP 10 MIN(productId) productId, productName,brandName,color, MIN(size) size, currencyCode,
                    imageUrlMain,MIN(altPrice) altPrice, MIN(unitPrice) unitPrice, MAX(lastUpdated) lastUpdated
                FROM tblViewedItem vw
                WHERE ISNULL(vw.deleted,0)=0 AND browserId='" + browserId + "'" +
                " GROUP BY productName,brandName,color,currencyCode,imageUrlMain ORDER BY lastUpdated DESC ").ToList();

            return Json(viewed);
        }
        */
        //-- declaration in Global.asax.cs  (MvcApplication_PostAuthenticateRequest)
        //-- code link [start]: https://stackoverflow.com/questions/11478244/asp-net-web-api-session-or-something 
        public void SetSession(string sessionName, object value)
        {
            try
            {
                System.Web.HttpContext.Current.Session[sessionName] = value;
            }
            catch (Exception ex)
            {
                string test = "";
            }
        }
        public object GetSession(string sessionName)
        {
            return System.Web.HttpContext.Current.Session[sessionName];
        }
        //-- code link [end]: https://stackoverflow.com/questions/11478244/asp-net-web-api-session-or-something 

    }
}
