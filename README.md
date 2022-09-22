# BenefitCardMVC
Projekt ze 7-hodinoveho hackathonu, ktery uzivateli zobrazi lokality, ve kterych muze vyuzivat Benefit kartu pro dane aktivity.

1. (Model) [Model obsahuje definice vsech trid, data](https://github.com/spiduso/BenefitCardMVC/tree/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Models) a [nacte Controlleru data lokalit z jsonu (idealne z DB),](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Models/Database.cs#L23) (Controller) [ktere si Controller uchova](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Controllers/HomeController.cs#L18)
2. (View) [Uzivatel ve View Index odklikne tlacitko, ze chce hledat aktivity](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Views/Home/Index.cshtml#L13) (Controller) [a Controller pripravi aktivity pro View ListActivities](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Controllers/HomeController.cs#L30)
3. (View) [Potom co si uzivatel vybere aktivity]() (Controller) [Controller mu najde prislusne lokality](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Controllers/HomeController.cs#L44) (View) [ktere ze zobrazi na View TableActivities](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Views/Home/TableActivities.cshtml#L1)
4. (View) Uzivatel muze rozkliklnout detaily lokality a [potom se mu zobrazi View ShowDetail, ktery dostane detaily o lokalite a zobrazi je](https://github.com/spiduso/BenefitCardMVC/blob/6722bda61eb0da4b445b59bdae1a2544ed12b0c2/BenefitCard/BenefitCard/Views/Home/ShowDetail.cshtml)

Demo video <br>
<video controls>
  <source src="demo_video.mp4" type="video/mp4">
</video>