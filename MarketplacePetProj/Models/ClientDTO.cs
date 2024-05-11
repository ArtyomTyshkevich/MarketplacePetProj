﻿using MarketplacePetProj.Enums;

namespace MarketplacePetProj.Models
{
    public class ClientDTO
    {
        public string Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string PhoneNum { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
        public ClientDTO(Client client, IWebHostEnvironment env)
        {
            this.Id =  client.Id;
            this.Name = client.UserName;
            this.PhoneNum = client.PhoneNumber;
            this.Description = client.Description;
            string filePath = Path.Combine(env.WebRootPath, "ClientImages", client.ImageName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                this.ImageFile = new FormFile(fileStream, 0, fileStream.Length, null, Path.GetFileName(filePath));
            }
        }
        public ClientDTO()
        {
            Name = "";
            Description = "";
            PhoneNum = "";
        }
    }
}