using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDVStore.Data;
using PDVStore.Models;

namespace PDVStore.Services
{
    public class VendaService
    {
        private readonly PDVContext _context;

        public VendaService(PDVContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> GetAllAsync()
        {
            return await _context.Vendas
                .Include(v => v.Items)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Venda?> GetByIdAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.Items)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var v = await _context.Vendas.FindAsync(id);
            if (v != null)
            {
                _context.Vendas.Remove(v);
                await _context.SaveChangesAsync();
            }
        }
    }
}
