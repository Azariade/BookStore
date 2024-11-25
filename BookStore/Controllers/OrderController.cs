using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Order/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Charger la liste des produits pour le formulaire
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public IActionResult Create(Order order, int[] productIds, int[] quantities)
        {
            if (productIds.Length != quantities.Length)
            {
                ModelState.AddModelError("", "Le nombre de produits doit correspondre au nombre de quantités.");
                ViewBag.Products = _context.Products.ToList();
                return View(order);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Liste des articles de commande
                    var orderItems = new List<OrderItem>();
                    decimal totalOrderAmount = 0;

                    for (int i = 0; i < productIds.Length; i++)
                    {
                        var productId = productIds[i];
                        var quantity = quantities[i];

                        // Vérifier l'existence du produit
                        var product = _context.Products.SingleOrDefault(p => p.ProductId == productId);
                        if (product == null)
                        {
                            ModelState.AddModelError("", $"Produit avec ID {productId} introuvable.");
                            continue;
                        }

                        // Vérifier la quantité en stock
                        if (quantity > product.StockQuantity)
                        {
                            ModelState.AddModelError("", $"Le produit '{product.Name}' n'a pas suffisamment de stock. Disponible : {product.StockQuantity}, requis : {quantity}.");
                            continue;
                        }

                        // Créer un article de commande
                        var orderItem = new OrderItem
                        {
                            Product = product,
                            Quantity = quantity,
                            Order = order,
                        };
                        orderItems.Add(orderItem);
                        totalOrderAmount += orderItem.TotalAmount;

                        // Réduire le stock du produit
                        product.StockQuantity -= quantity;
                        _context.Products.Update(product);
                    }

                    // Vérifier les erreurs de validation
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Products = _context.Products.ToList();
                        transaction.Rollback(); // Annuler les modifications
                        return View(order);
                    }

                    // Sauvegarder la commande
                    order.OrderDate = DateTime.Now;
                    order.TotalAmount = totalOrderAmount;
                    _context.Orders.Add(order);
                    _context.SaveChanges(); // Sauvegarder la commande pour obtenir son ID

                    // Associer les articles à la commande et sauvegarder
                    foreach (var item in orderItems)
                    {
                        item.OrderId = order.OrderId;
                        _context.OrderItems.Add(item);
                    }
                    _context.SaveChanges();

                    // Confirmer la transaction
                    transaction.Commit();

                    return RedirectToAction("Details", new { id = order.OrderId });
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Annuler la transaction en cas d'erreur
                    ModelState.AddModelError("", "Une erreur s'est produite lors de la création de la commande.");
                    ViewBag.Products = _context.Products.ToList();
                    return View(order);
                }
            }
        }

    }

}



