﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ComputePromotion
/// </summary>
public class ComputePromotion
{
    //returns PromotionID and count of promotions
    public static decimal GetTotalPrice(Order ord, Promotion prom)
    {
        decimal d = 0M;
        //get count of promoted products in order
        var copp = ord.Products
            .GroupBy(x => x.Id)
            .Where(grp => prom.ProductInfo.Any(y => grp.Key == y.Key && grp.Count() >= y.Value))
            .Select(grp => grp.Count())
            .Sum();

        //get count of promoted products from promotion
        int ppc = prom.ProductInfo.Sum(kvp => kvp.Value);
        while (copp >= ppc)
        {
            d += prom.PromoPrice;
            copp -= ppc;
        }
        if (d != 0M)
        {
            d = d + copp * new Product(prom.ProductInfo.Keys.FirstOrDefault()).Price;
        }
        else
        {
            d = d + new Product(prom.ProductInfo.Keys.FirstOrDefault()).Price;
        }

        return d;
    }
}