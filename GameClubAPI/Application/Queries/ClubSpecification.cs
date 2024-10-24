using Domain.Clubs;
using Infrastructure.Repository;
using System.Linq.Expressions;

namespace Application.Queries
{
    public class ClubSpecification : SpecificationBase<Club>
    {
        public ClubSpecification(bool isInclude = true, int id = 0, string? name = "", string? description = "", string? searchValue = "") : base(isInclude)
        {

            // search by id
            if(id > 0){
                Expression<Func<Club, bool>> criteria = c =>
                    id.Equals(c.Id);

                AddCriteria(criteria);
            }

            // search all text columns
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToLower().Trim();
                Expression<Func<Club, bool>> criteria = c =>
                    searchValue.Contains(c.Name.ToLower() ?? "") || searchValue.Contains(c.Description.ToLower() ?? "");

                AddCriteria(criteria);
            }

            // search by name
            if(!string.IsNullOrEmpty(name)){
                name = name.ToLower().Trim();
                Expression<Func<Club, bool>> criteria = c =>
                    name.Contains(c.Name.ToLower() ?? "");

                AddCriteria(criteria);
            }

            // search by description
            if(!string.IsNullOrEmpty(description)){
                description = description.ToLower().Trim();
                Expression<Func<Club, bool>> criteria = c =>
                    description.Contains(c.Description.ToLower() ?? "");

                AddCriteria(criteria);
            }

            // include related entity
            if (IsInclude)
            {
                AddInclude(w => w.Events);
            }
        }
    }
}
