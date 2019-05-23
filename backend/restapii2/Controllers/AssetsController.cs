using restapii2.Context;
using restapii2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace restapii2.Controllers
{
    //[Authorize]
    public class AssetsController : ApiController
    {
        // GET: api/assets
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get()
        {
            IEnumerable<tblAsset> assets = Enumerable.Empty<tblAsset>();
            RestApii2Context db = new RestApii2Context();
            try
            {
                string sql = @"SELECT * FROM tblAsset ast WHERE ISNULL(deleted,0)=0";
                assets = db.Database.SqlQuery<tblAsset>(sql).ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            finally { db.Dispose(); }
            return Json(new { assets });
        }
          

        // GET: api/assets/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get(int id)
        {
            RestApii2Context db = new RestApii2Context();
            IEnumerable<tblAsset> assets = Enumerable.Empty<tblAsset>();
            try
            {
                string sql = @"SELECT * FROM tblAsset ast WHERE ISNULL(deleted,0)=0 AND assetId=" + id;
                assets = db.Database.SqlQuery<tblAsset>(sql).ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            finally { db.Dispose(); }
            return Json(new { assets });
        }

        // POST: api/assets
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]tblAsset asset)
        {
            RestApii2Context db = new RestApii2Context();
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
                if (bAdd)
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

        // PUT: api/assets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //-- DELETE fuunction only marks the record as DELETED for auditing purposes
        // DELETE: api/assets/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            RestApii2Context db = new RestApii2Context();
            try
            {
                string sql = @"UPDATE tblAsset SET deleted=1 WHERE assetId=" + id;
                db.Database.ExecuteSqlCommand(sql);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            finally { db.Dispose(); }
            return await Get();
        }

        [HttpPost]
        [Route("~/asset/save")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> saveAsset([FromBody]tblAsset asset)
        {
            RestApii2Context db = new RestApii2Context();
            try
            {
                tblAsset tAsset = db.Assets.Where(i => i.assetId == asset.assetId).FirstOrDefault();

                //-- copy data from one model to another different model
                ObjectExtension.ObjectCopy(asset, tAsset, false);

                tAsset.lastUpdated = DateTime.Now;
                if (tAsset == null)
                {
                    tAsset.dateCreated = DateTime.Now;
                    db.Assets.Add(tAsset);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            finally { db.Dispose(); }
            return await Get();
        }
    }
}
