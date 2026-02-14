namespace TP5.Models
{
    public class PanierParUser
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public Guid ProduitId { get; set; }
        public List<Produit> produits { get; set; }

    }
}
