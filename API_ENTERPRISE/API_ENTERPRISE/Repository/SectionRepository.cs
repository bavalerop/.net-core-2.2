using API_ENTERPRISE.Data;
using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Repository
{
    public class SectionRepository : ISectionRepository
    {

        private readonly TodoContext _context;

        public SectionRepository(TodoContext context) {
            this._context = context;
        }
        public async Task<QueryResult<ResponsSection>> GetSection()
        {
            var result = new QueryResult<ResponsSection>();

            try
            {
                    var section = (from sec in this._context.section
                               select new ResponsSection
                               {
                                   id = Convert.ToInt16(sec.id),
                                   abbreviation = sec.abbreviation,
                                   name = sec.name.Trim(),
                                   state = sec.estado == 1 ? true : false
                               }

                              );
                    //Alamacena una lista de sedes en el objeto QueryResult
                    result.Items = await section.ToListAsync();
                    //Se retorna la lista
                    return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Se retorna la lista
            return result;
        }

        public async Task<QueryResult<ResponsSection>> GetSectionByID(int id)
        {
            var result = new QueryResult<ResponsSection>();

            try
            {
                    var section = (from sec in this._context.section
                               where sec.id == id
                               select new ResponsSection
                               {
                                   id = sec.id,
                                   abbreviation = sec.abbreviation,
                                   name = sec.name.Trim(),
                                   state = sec.estado == 1 ? true : false
                               }

                              );
                    //Alamacena una lista de sedes en el objeto QueryResult
                    result.Items = await section.ToListAsync();
                    //Se retorna la lista
                    return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Se retorna la lista
            return result;
        }
    }
}
