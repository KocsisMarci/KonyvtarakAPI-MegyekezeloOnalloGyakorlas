using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KonyvtarakAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace KonyvtarakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MegyekController : ControllerBase
    {

        public KonyvtarakContext _context { get; set; }

        public MegyekController(KonyvtarakContext context) {
            _context = context;
        }

        [HttpGet("GetAllMegye")]

        public IActionResult GetAllMegye() {
            try
            {
                return Ok(_context.Megyeks.ToList());
            }

            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }
        }

        [HttpGet("GetAllAsyncMegye")]

        public async Task<IActionResult> GetAllAsyncMegye()
        {
            try
            {
                return  Ok(await _context.Megyeks.ToListAsync());

            }

            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
             }
        }


        [HttpGet("GetMegyeById/{id}")]

        public IActionResult GetMegyeById(int id) {
            try
            {

                var adott = _context.Megyeks.FirstOrDefault(megye => megye.Id == id);

                if (adott is not null) {
                    return Ok(adott);
                }

                else
                {
                    return NotFound("Nincs ilyen id-jú megye");
                }
            }


            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }

        }


        [HttpGet("GetAsyncMegyeById/{id}")]

        public async Task<IActionResult> GetAsyncMegyeById(int id) {
            try{
                var adott = await _context.Megyeks.ToListAsync();

                var aJo =  adott.FirstOrDefault(megye => megye.Id == id);

                if (aJo is not null) {
                    return Ok(aJo);

                }

                else
                {
                    return NotFound("Nincs ilyen idjú megye");

                }

            }


            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }

        }
 
        [HttpPost("PostMegye")]

        public IActionResult PostMegye(Megyek megye)
        {
            try
            {
                _context.Megyeks.Add(megye);
                _context.SaveChanges();
                return Ok("Sikeres post történt");
            }

            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a  post során, bővebben: {ex.Message}");
            }
        }

        [HttpPost("PostAsyncMegye")]

        public async Task<IActionResult> PostAsyncMegye(Megyek megye)
        {
            try
            {
                await _context.Megyeks.AddAsync(megye);
                await _context.SaveChangesAsync();

                return Ok("Sikeres post");
            }


            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }
        }


        [HttpPut("PutMegye/{id}")]

        public IActionResult PutMegye(Megyek megye, int id) {
            try
            {
                var toUpdate = _context.Megyeks.FirstOrDefault(megye => megye.Id == id);


                _context.ChangeTracker.Clear();
                if(toUpdate is not null)
                {
                    _context.Update(megye);
                    _context.SaveChanges();
                    return Ok("Sikeres put");

                }

                else
                {
                    return NotFound("Nincs ilyen id-val rendelkező megye");
                }
            }

            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a kérés során,  bővebben: {ex.Message}");
            }
        }


        [HttpPut("PutAsyncMegye/{id}")]

        public async Task<IActionResult> PutAsyncMegye(int id, Megyek megye)
        {
            try
            {
                var adott = _context.Megyeks.FirstOrDefault(megye => megye.Id == id);

                _context.ChangeTracker.Clear();

                if (adott != null) {
                    _context.Update(megye);
                    await _context.SaveChangesAsync();

                    return Ok("Sikeres put");
                }

                else
                {
                    return NotFound("Nics ilyen id-val rendelkező megye");
                }
            }

            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }
        }


       


        [HttpDelete("deleteMegye/{id}")]

        public IActionResult DeleteMegye(int id)
        {
            try
            {
                var toDelete = _context.Megyeks.FirstOrDefault(megye => megye.Id == id);

                _context.ChangeTracker.Clear();

                if (toDelete is not null)
                {
                    _context.Megyeks.Remove(toDelete);
                    _context.SaveChanges();
                    return Ok("Sikeres delete");
                }

                else
                {
                    return NotFound("Nincs ilyen id-jú megye");
                }

            }


            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }
        }




        [HttpDelete("DeleteAsyncMegye/{id}")]

        public async Task<IActionResult> DeleteAsyncMegye(int id)
        {
            try
            {
                var toDelete = _context.Megyeks.ToList().FirstOrDefault(megye => megye.Id == id);

                _context.ChangeTracker.Clear();

                if (toDelete is null) {
                    return NotFound("Nincsen megye ilyen id-val");
                }

                else
                {
                     _context.Remove(toDelete);
                    await _context.SaveChangesAsync();

                    return Ok("Sikeres törlés");
                }

            }

            catch (Exception ex) {
                return StatusCode(500, $"Hiba történt a kérés során, bővebben: {ex.Message}");
            }

        
        }

        [HttpGet("GetOsszekapcsolt")]

        public IActionResult OsszeKapcsolt() {
            return Ok(_context.Telepuleseks.Include(t => t.Konyvtaraks).ToList());
        }
        
    }
}
