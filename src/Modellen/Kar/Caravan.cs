﻿namespace WPRRewrite2.Modellen.Kar;

public class Caravan(string kenteken, string merk, string model, string kleur, int aanschafjaar, int prijs, string brandstofType)
    : Voertuig(kenteken, merk, model, kleur, aanschafjaar, prijs, brandstofType);