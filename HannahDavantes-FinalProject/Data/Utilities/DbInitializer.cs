using HannahDavantes_FinalProject.Data.Enums;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.Utilities {
    public class DbInitializer {
        /// <summary>
        /// This method is used to add data into the database.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void LoadInitialData(IApplicationBuilder applicationBuilder) {

            //Get the DBContext through the list of services of the app
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<DbContextUtility>();


                //Uncomment to delete database then recreate and repopulate with data
                context.Database.EnsureDeleted();

                //Check if database exists, if not then the database will be created first
                context.Database.EnsureCreated();

                //Add Products if products table is empty
                if (!context.Products.Any()) {
                    context.Products.AddRange(new List<Product>() {
                        new Product() {
                            Name = "Bye Bye Blackhead 30 Days Miracle Green Tea Tox Bubble Cleanser",
                            Brand = "Some By Mi",
                            Category =  ProductCategory.Cleanser,
                            SizeNumber = 120,
                            SizeUnit = ProductSizeUnit.g,
                            Price = 17.85,
                            Description = "A convenient 5-minute bubble cleanser that cares blackheads, whiteheads, and skin wastes in the pores. " +
                                        "The acidulous cleanser exfoliates the skin mildly with natural BHA, green tea leaf, and konjac particles. " +
                                        "16 tea ingredients and Tannin Complex tightens and firms the widened pores with healthy elasticity.",
                            Photo = "default/20fb9fa8-0471-45d0-81e7-79fe03edbabb.jpg"
                        },
                       new Product() {
                            Name = "Blueberry Rebalancing 5.5 Cleanser",
                            Brand = "Innisfree",
                            Category =  ProductCategory.Cleanser,
                            SizeNumber = 100,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 10.75,
                            Description = "An acidulous foam cleanser that adjusts the pH level of the skin healthily with smooth bubbles." +
                                        "Minimizes skin stress during face washes by adjusting the pH level and oil-moisture balance of the skin. " +
                                        "Protects the moisture and lipid membrane of the skin, tending the skin moistly and softly without tautness. " +
                                        "Contains moist and anti-oxidant blueberry, in season and rich with minerals, to convey the vitality and energy of nature.",
                            Photo = "default/3dfb6586-6957-4410-9a36-3dede2a8e367.jpg"
                        },
                        new Product() {
                            Name = "Super Aqua Ultra Hyalron Cleansing Foam",
                            Brand = "Missha",
                            Category =  ProductCategory.Cleanser,
                            SizeNumber = 200,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 11.60,
                            Description = "The ample micro-bubbles of the mild cleansing foam cleanses the skin smoothly and moistly. " +
                                        "10 Hyaluronic Acids of various size gather ample moisture, keeping the skin moist after cleansing. " +
                                        "The hypo-allergenic formula of the moisturizing ingredients cares the skin mildly and healthily. ",
                            Photo = "default/e328d2b7-35bc-46e6-93ea-634d27c47917.jpg"
                        },
                        new Product() {
                            Name = "Low pH Good Morning Gel Cleanser",
                            Brand = "COSRX",
                            Category =  ProductCategory.Cleanser,
                            SizeNumber = 150,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 10.60,
                            Description = "1. Low pH that is similar to that of skin. " +
                                        "2. Natural BHA exfoliates skin. " +
                                        "3. Tea Tree Oil controls oil and tightens pores. "+
                                        "4. Mild foam cleanser for sensitive skin. "+
                                        "5.Moist.Skins don't pull each other.",
                            Photo = "default/f729f2d8-5ce7-4926-9e0b-81dcdd27696f.jpg"
                        },
                        new Product() {
                            Name = "Aloe Pure Cleansing Foam",
                            Brand = "Farmstay",
                            Category =  ProductCategory.Cleanser,
                            SizeNumber = 180,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 8.55,
                            Description = "Various natural ingredients like aloe, olive, and green tea cleanse the skin without stimulation. " +
                                        "Ample, smooth, and fine bubbles deep-cleanse and eliminate makeup residue and fine dust." +
                                        "Aloe extract conveys skin-soothing moisture, tending the dry skin healthily with vitamins and minerals.",
                            Photo = "default/7b158021-94bf-4187-b07f-cb1ad9c0aacd.jpg"
                        },
                        new Product() {
                            Name = "Essence Toner",
                            Brand = "Pyunkang Yul",
                            Category =  ProductCategory.Toner,
                            SizeNumber = 100,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 12.30,
                            Description = "The thick texture cares the skin moist and smooth. " +
                                        " Easily absorbs the wastes and wipe the skin clean." +
                                        " Tends the skin healthily by raising skin temperature.",
                            Photo = "default/87b0cc93-98df-4bc3-8dd0-5fb0b4c9eced.jpg"
                        },
                        new Product() {
                            Name = "Beauty Water",
                            Brand = "SON&PARK",
                            Category =  ProductCategory.Toner,
                            SizeNumber = 340,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 22.90,
                            Description = "It is a product that removes unhardened keratin or unremoved cleansing residues " +
                                        " when using conventional toner, and toning and moisturizing to make the next step easier. " +
                                        " Contains a full range of naturally derived ingredients to make your skin look lively and vibrant without irritation.",
                            Photo = "default/cd6bcef7-259a-4398-83c9-88e23a33abcf.jpg"
                        },
                        new Product() {
                            Name = "Rose Water",
                            Brand = "Mamonde",
                            Category =  ProductCategory.Toner,
                            SizeNumber = 500,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 28.50,
                            Description = "Rose Water Toner with soothing and moisturizing effects, offering greater hydration with each wipe on your skin. " +
                                        " 90.97% Real Rose Water. Damask Roses are harvested in their finest state to maintain freshness and scent. " +
                                        " Antioxidant-Rich Formula Rose Water Toner's antioxidant-rich formula helps visibly soothe, hydrate, refresh and purify your skin complexion.",
                            Photo = "default/d8285082-3f2c-4266-b339-19092ca47c82.jpg"
                        },
                        new Product() {
                            Name = "Royal Honey Propolis Enrich Cream Mist",
                            Brand = "SKINFOOD",
                            Category =  ProductCategory.Toner,
                            SizeNumber = 120,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 24.00,
                            Description = "A moisturizing cream mist formulated with Propolis Extract to rejuvenate the skin for a healthier complexion. " +
                                        " Contains Cera-Green Complex and the special ingredient Pro-shield (more concentrated Propolis Extract) to strengthen skin barrier and Moisturizes the skin with Panthenol. " +
                                        " Fine mist is enriched with a liquid emulsion evenly and gently deliver nutrients and hydration to skin.",
                            Photo = "default/5fe7c0f7-1ff1-4335-848b-ee4bedae33fc.jpg"
                        },
                        new Product() {
                            Name = "Galac Whitening Vita Toner",
                            Brand = "Manyo Factory",
                            Category =  ProductCategory.Toner,
                            SizeNumber = 210,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 16.15,
                            Description = "THE ULTIMATE DAILY MULTIVITAMINS FOR YOUR SKIN!! " +
                                        " The secret to bright and radiant skin is an efficient exfoliation that contains AHA, BHA, PHA, and LHA. " +
                                        " 12 Vita Complex: All-in-One Solution for various skin problems.",
                            Photo = "default/3bd81a80-f485-4ba8-9898-1de58f3fb048.jpg"
                        },
                        new Product() {
                            Name = "Gold CF-Nest Extract 97 B-jo Serum",
                            Brand = "Elizavecca",
                            Category = ProductCategory.Serum,
                            SizeNumber = 50,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 8.90,
                            Description = "A whitening and anti-aging serum that helps repair the skin with EGF and Swallow's Nest Extract. " +
                                        " Improve the skin texture and moistness with the healthy and vitalizing elasticity of the clear serum. ",
                            Photo = "default/e92b664a-e4e1-4d44-b8bd-9241e93aa915.jpg"
                        },
                        new Product() {
                            Name = "Galactomyces Pure Vitamin C Glow Serum",
                            Brand = "Some By Mi",
                            Category = ProductCategory.Serum,
                            SizeNumber = 30,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 15.80,
                            Description = " A serum is formulated with 70% propolis extract and 12% vitamin C that calms and brightens the skin." +
                                        " Propolis containing anti-inflammatory properties that makes skin radiant. " +
                                        " It effectively controls pores and reduces sebum, leaving your skin brighter without unwanted shine. ",
                            Photo = "default/302757f5-4ca8-4431-a565-cd0c825ff99d.jpg"
                        },
                        new Product() {
                            Name = "Ceramidin Serum",
                            Brand = "Dr Jart+",
                            Category = ProductCategory.Serum,
                            SizeNumber = 40,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 41.50,
                            Description = "High-consumption five-column filling serum can be used with ease even for sensitive skin " +
                                        " Multi-Composites/Multi-Correction/Multi-services protect the skin barrier. " +
                                        " This serum is serricotype without stickiness and fully moisturizes the skin." ,
                            Photo = "default/69625f01-0edf-46ac-b89d-ff0b500a438c.jpg"
                        },
                        new Product() {
                            Name = "Double Effect Pore RX Tightening Serum",
                            Brand = "TOSOWOONG",
                            Category = ProductCategory.Serum,
                            SizeNumber = 30,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 17.55,
                            Description = "Dual Functional Pore Tightening Serum in Whitening + Anti wrinkle. " +
                                        "Tosowoong Pore RX Tightening Serum takes care of skin to be clear " +
                                        " and soft and boosts skin elasticity as containing functional ingredients." +
                                        " As containing Camellia Japonica Flower Extract, a functional ingredient," +
                                        " it takes care of skin textures to be silky and tightens pores effectively. " ,
                            Photo = "default/3aa32aaa-4aac-4be5-9182-fd276659b20a.jpg"
                        },
                        new Product() {
                            Name = "Revive Serum",
                            Brand = "Beauty of Joseon",
                            Category = ProductCategory.Serum,
                            SizeNumber = 30,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 22.75,
                            Description = "It promotes the regeneration of skin cells to help restore damaged skin and prevent skin aging. " +
                                        " Licorice contains full of anti-inflammatory properties that help reduce eczema, psoriasis, dermatitis. " +
                                        " It was commonly used as a herbal medicine in joseon era. It's effective for skin brightening for skin brightening and anti-wrinkle. ",
                            Photo = "default/72a877e5-f6d4-4a71-b755-ccfbc8079b4d.jpg"
                        },
                        new Product() {
                            Name = "UV Double Cut Aqua Sun Essence SPF 50+ PA++++",
                            Brand = "Etude House",
                            Category = ProductCategory.Suncare,
                            SizeNumber = 50,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 12.75,
                            Description = "Fresh moisturizing sun essence without stickiness. " +
                                        " UV Double CUT that double-products against the hot sun & UV rays. " +
                                        " Sun Essence that fills the skin with freshness & moisture. ",
                            Photo = "default/37681805-d9be-4595-b1e6-e21152a9b4ca.jpg"
                        },
                        new Product() {
                            Name = "Aloe Waterproof Sun Cream",
                            Brand = "HOLIKA HOLIKA",
                            Category = ProductCategory.Suncare,
                            SizeNumber = 70,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 20.00,
                            Description = "Despite any information provided by the seller, this product is not intended for the diagnosis, cure, mitigation, treatment or prevention of any disease. " +
                                        " Prevents the appearance of oily luster, controls the functioning of the sebaceous glands.Preserves and maintains the freshness of the skin tone. " +
                                        " Polysaccharides of aloe, which are distributed over the surface of the skin, create a moisturizing film. ",
                            Photo = "default/37681805-d9be-4595-b1e6-e21152a9b4ca.jpg"
                        },
                        new Product() {
                            Name = "Safe Block RX Cover Tone Up Sun SPF50+ PA++++",
                            Brand = "MISSHA",
                            Category = ProductCategory.Suncare,
                            SizeNumber = 50,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 19.15,
                            Description = "A powerful whitening and anti-aging sun cream that blocks harmful fine dust and UV rays from entering the skin. " +
                                        " The physical sun cream with hypoallergenic formula cools and soothes the heated skin without stimulation. " +
                                        " The fresh and delicate sun cream covers the skin smoothly with the natural ivory color, brightening the skin tone. ",
                            Photo = "default/118ff342-b1b6-497e-905c-59ce2bbbf8ba.jpg"
                        },
                        new Product() {
                            Name = "Intensive Long-lasting Sunscreen SPF50+ PA++++",
                            Brand = "Innisfree",
                            Category = ProductCategory.Suncare,
                            SizeNumber = 50,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 18.25,
                            Description = "Strong waterproof & non-sticky texture type sunscreen. " +
                                        " Calm & moisturize skin effectively. " +
                                        " Non-greasy type sunscreen providing fresh finish. ",
                            Photo = "default/090bb542-1324-45f3-90be-9a961cd5578b.jpg"
                        },
                        new Product() {
                            Name = "Daily Soft Sun Milk",
                            Brand = "SWANICOCO",
                            Category = ProductCategory.Suncare,
                            SizeNumber = 50,
                            SizeUnit = ProductSizeUnit.ml,
                            Price = 14.25,
                            Description = "The fresh and soft milk texture of the mild physical sunscreen adheres smoothly to the skin. " +
                                        " Herb Complex and Aqua Moisture 24 Complex strengthens the skin barrier. " +
                                        " The powerful SPF50+ PA++++ sunscreen blocks UV rays and protects the skin. ",
                            Photo = "default/0ba9bc08-89fb-495a-8636-023f293c40a8.jpg"
                        }

                    });
                }
            }
        }
    }
}
