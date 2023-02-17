using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class DocumentController : BaseApiController
    {
        private readonly DataContext _context;
        public DocumentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var documents = await _context.Document.ToListAsync();

            return documents;
        }

        [HttpGet("id")]

        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);

            return document;
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateDocument(DocumentDto documentDto)
        {
            var document = new Document
            {
                DocumentName = documentDto.DocumentName,
                LocationUrl = documentDto.LocationUrl
            };

            _context.Document.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDocument),
                new { id = document.Id},
                (document));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Document.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}