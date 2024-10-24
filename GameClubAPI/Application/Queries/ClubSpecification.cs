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
                Expression<Func<Club, bool>> criteria = c => id.Equals(c.Id);
                AddCriteria(criteria);
            }

            // search all text columns
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToLower().Trim();
                Expression<Func<Club, bool>> criteria = c =>
                    c.Name.ToLower().Trim().Contains(searchValue)
                    || c.Description.ToLower().Trim().Contains(searchValue);

                AddCriteria(criteria);
            }

            // search by name
            if(!string.IsNullOrEmpty(name)){
                name = name.ToLower().Trim();
                Expression<Func<Club, bool>> criteria = c =>
                    c.Name.ToLower().Trim().Contains(name);

                AddCriteria(criteria);
            }

            // search by description
            if(!string.IsNullOrEmpty(description)){
                description = description.ToLower().Trim();
                Expression<Func<Club, bool>> criteria = c =>
                    c.Description.ToLower().Trim().Contains(description);

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
