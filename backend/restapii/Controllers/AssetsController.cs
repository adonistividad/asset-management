using restapii.Context;
using restapii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace restapii.Controllers
{
    public class AssetsController : ApiController
    {
        // GET: api/assets
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get()
        {
            RestApiiContext db = new RestApiiContext();
            string sql = @"SELECT * FROM tblAsset ast WHERE ISNULL(deleted,0)=0";
            IEnumerable<tblAsset> assets =  db.Database.SqlQuery<tblAsset>(sql).ToList();

            return Json(new { assets });
        }

        // GET: api/assets/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get(int id)
        {
            RestApiiContext db = new RestApiiContext();
            string sql = @"SELECT * FROM tblAsset ast WHERE ISNULL(deleted,0)=0 AND assetId=" + id;
            IEnumerable<tblAsset> assets = db.Database.SqlQuery<tblAsset>(sql).ToList();

            return Json(new { assets });
        }

        // POST: api/assets
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]tblAsset asset)
        {
            RestApiiContext db = new RestApiiContext();
            try
            {
                bool bAdd = false;
                tblAsset tAsset = db.Assets.Where(i => i.assetId == asset.assetId).FirstOrDefault();
                if (tAsset == null)
                {
                    tAsset = new tblAsset();
                    bAdd = true;
                }

                //-- copy data from one model to another different model
                ObjectExtension.ObjectCopy(asset, tAsset, false);

                tAsset.lastUpdated = DateTime.Now;
                if (bAdd){
                    db.Assets.Add(tAsset);
                }
                db.SaveChanges();



            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            finally
            {
                db.Dispose();
            }
            return await Get();
        }

        // PUT: api/assets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //-- DELETE fuunction only marks the record as DELETED for auditing purposes
        // DELETE: api/assets/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            RestApiiContext db = new RestApiiContext();
            string sql = @"UPDATE tblAsset SET deleted=1 WHERE assetId=" + id;
            db.Database.ExecuteSqlCommand(sql);
            return await Get();
        }

        [HttpPost]
        [Route("~/asset/save")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> saveAsset([FromBody]tblAsset asset)
        {
            RestApiiContext db = new RestApiiContext();
            try
            {
                tblAsset tAsset = db.Assets.Where(i => i.assetId == asset.assetId).FirstOrDefault();

                //-- copy data from one model to another different model
                ObjectExtension.ObjectCopy(asset, tAsset, false);

                tAsset.lastUpdated = DateTime.Now;
                if (tAsset == null)
                {
                    db.Assets.Add(tAsset);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            finally
            {
                db.Dispose();
            }
            return await Get();
        }

    }
}
