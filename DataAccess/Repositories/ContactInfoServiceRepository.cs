using StoreApp.DataAccess;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace DataAccess.Repositories;

public class ContactInfoServiceRepository(StoreDbContext context) : IContactInfoService<ContactInfo>
{
    public async Task<IEnumerable<ContactInfo>> GetAllAsync()
    {
        return context.ContactInfo;
    }

    public async Task<ContactInfo> GetByIdAsync(int id)
    {
		return await context.ContactInfo.FindAsync(id);

    }

    public async Task AddAsync(ContactInfo entity)
    {
        await context.ContactInfo.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var deleteContactInfo = await context.ContactInfo.FindAsync(id);
        context.ContactInfo.Remove(deleteContactInfo);
    }

    public async Task<ContactInfo> UpdateAsync(int id, ContactInfo entity)
    {
        var contactInfo = await GetByIdAsync(id);

        if (contactInfo is null)
        {
            return null;
        }

        contactInfo.Address = entity.Address;
        contactInfo.City = entity.City;
        contactInfo.ZipCode = entity.ZipCode;
        contactInfo.Country = entity.Country;
        contactInfo.Phone = entity.Phone;
        contactInfo.Region = entity.Region;
        
        return entity;
    }
}