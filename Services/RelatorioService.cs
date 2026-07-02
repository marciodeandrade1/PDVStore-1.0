using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PDVStore.Data;
using PDVStore.Models;

namespace PDVStore.Services
{
    public class RelatorioService
    {
        private readonly PDVContext _context;

        public RelatorioService(PDVContext context)
        {
            _context = context;
        }

        public List<ItemRelatorio> GerarRelatorioItensMaisVendidos(DateTime inicio, DateTime fim, int topN = 10)
        {
            var query = _context.ItensVenda
                .Include(iv => iv.Produto)
                .Where(iv => iv.Venda.Data >= inicio && iv.Venda.Data <= fim)
                .GroupBy(iv => iv.Produto)
                .Select(g => new ItemRelatorio
                {
                    NomeProduto = g.Key.Nome,
                    TotalVendido = g.Sum(iv => iv.Quantidade),
                    EstoqueAtual = g.Key.QuantidadeEstoque,
                    StatusMinimo = g.Key.QuantidadeEstoque < g.Key.EstoqueMinimo ? "Baixo" : "OK"
                })
                .OrderByDescending(ir => ir.TotalVendido)
                .Take(topN)
                .ToList();

            return query;
        }

        public List<Produto> GerarRelatorioEstoqueMinimo()
        {
            return _context.Produtos
                .Where(p => p.QuantidadeEstoque < p.EstoqueMinimo)
                .ToList();
        }

        /// <summary>
        /// Exporta relatório para PDF usando iText 9.6.0
        /// </summary>
        public void ExportarPDF<T>(List<T> dados, string caminho) where T : class
        {
            using (var writer = new PdfWriter(caminho))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                // Título do documento
                document.Add(new Paragraph("Relatório PDV")
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20));

                if (dados == null || !dados.Any())
                {
                    document.Add(new Paragraph("Nenhum dado encontrado para o período informado.")
                        .SetFontSize(12));
                    return;
                }

                // Tratamento específico para ItemRelatorio
                if (typeof(T) == typeof(ItemRelatorio))
                {
                    var table = new Table(UnitValue.CreatePercentArray(new float[] { 45, 18, 18, 19 }))
                        .SetWidth(UnitValue.CreatePercentValue(100));

                    // Cabeçalhos
                    var headerColor = ColorConstants.LIGHT_GRAY;

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Produto")).SetBackgroundColor(headerColor));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Total Vendido")).SetBackgroundColor(headerColor).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Estoque Atual")).SetBackgroundColor(headerColor).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Status")).SetBackgroundColor(headerColor).SetTextAlignment(TextAlignment.CENTER));

                    foreach (var item in dados.Cast<ItemRelatorio>())
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.NomeProduto)));
                        table.AddCell(new Cell().Add(new Paragraph(item.TotalVendido.ToString()))
                            .SetTextAlignment(TextAlignment.CENTER));
                        table.AddCell(new Cell().Add(new Paragraph(item.EstoqueAtual.ToString()))
                            .SetTextAlignment(TextAlignment.CENTER));

                        var statusCell = new Cell().Add(new Paragraph(item.StatusMinimo))
                            .SetTextAlignment(TextAlignment.CENTER);

                        if (item.StatusMinimo == "Baixo")
                            statusCell.SetFontColor(ColorConstants.RED);

                        table.AddCell(statusCell);
                    }

                    document.Add(table);
                }
                // Tratamento para Relatório de Estoque Mínimo
                else if (typeof(T) == typeof(Produto))
                {
                    var table = new Table(UnitValue.CreatePercentArray(new float[] { 40, 20, 20, 20 }))
                        .SetWidth(UnitValue.CreatePercentValue(100));

                    var headerColor = ColorConstants.LIGHT_GRAY;

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Produto")).SetBackgroundColor(headerColor));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Estoque Atual")).SetBackgroundColor(headerColor).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Estoque Mínimo")).SetBackgroundColor(headerColor).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Status")).SetBackgroundColor(headerColor).SetTextAlignment(TextAlignment.CENTER));

                    foreach (var produto in dados.Cast<Produto>())
                    {
                        table.AddCell(new Cell().Add(new Paragraph(produto.Nome)));
                        table.AddCell(new Cell().Add(new Paragraph(produto.QuantidadeEstoque.ToString())).SetTextAlignment(TextAlignment.CENTER));
                        table.AddCell(new Cell().Add(new Paragraph(produto.EstoqueMinimo.ToString())).SetTextAlignment(TextAlignment.CENTER));
                        table.AddCell(new Cell().Add(new Paragraph("BAIXO"))
                            .SetFontColor(ColorConstants.RED)
                            .SetTextAlignment(TextAlignment.CENTER));
                    }

                    document.Add(table);
                }
                else
                {
                    // Fallback genérico
                    document.Add(new Paragraph($"Relatório de {typeof(T).Name} - {dados.Count} registros"));
                }

                // Rodapé com data de geração
                document.Add(new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetMarginTop(30));
            }
        }

        public void ExportarExcel<T>(List<T> dados, string caminho) where T : class
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // ou Commercial

            using (var package = new ExcelPackage(new FileInfo(caminho)))
            {
                var ws = package.Workbook.Worksheets.Add("Relatorio");

                // Implementação do Excel (mantida simples)
                if (dados.Any())
                {
                    // Preencher headers e dados aqui conforme necessário
                }

                package.Save();
            }
        }
    }

    public class ItemRelatorio
    {
        public string NomeProduto { get; set; }
        public int TotalVendido { get; set; }
        public int EstoqueAtual { get; set; }
        public string StatusMinimo { get; set; }
    }
}
