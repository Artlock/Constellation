using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class NameGenerator
{
    public string[] allNames = {
        "Titus", "Accius", "Gaius", "Acilius", "Claudia", "Acte", "Claudius", "Aelianus", "Sextus", "Aelius", "Paetus", "Catus", "Lucius", "Aelius", "Aemilia", "Scaura", "Marcus", "Aemilius", "Aemilianus", "Flavius", "Aetius",
        "Gnaeus", "Domitius", "Afer", "Lucius", "Afranius", "Julius", "Africanus", "Caecilius", "Agathinus", "Agricola", "Calpurnius", "Agrippa", "Vipsanius", "Postumus", "Agrippina", "Servilius", "Ahala", "Ahenobarbus",
        "Locutius", "Albinovanus", "Pedo", "Albucius", "Silus", "Varus", "Avitus", "Allectus", "Amafinius", "Turpio", "Gentilianus", "Ammianus", "Marcellinus", "Liber", "Memorialis", "Vinicianus", "Annius", "Antistius", "Vetus",
        "Antoninus", "Pius", "Arrius", "Iullus", "Liberalis", "Antyllus", "Creticus", "Castor", "Musa", "Diogenes", "Aper", "Festus", "Aphthonius", "Apicius", "pontius", "Aquila", "Manius", "Aquillius", "Arcadius", "Apronius", "Aulus",
        "Licinius", "Archias", "Arellius", "Fuscus", "Arria", "Major", "Caecina", "Arruntius", "Celsus", "Quintus", "Junius", "Arulenus", "Rusticus", "Arusianus", "Messius", "Asconius", "Preatextatus", "Philologus", "Atia", "Atilius",
        "Publius", "Caecilia", "Attica", "Pomponius", "Atticus", "Baebius", "Tamphilus", "Balbilus", "Cealius", "Atius", "Balista", "Bassus", "Bavius", "Belisarius", "Bestia", "Bibaculus", "Blossius", "Boethius", "Bolanus", "Bonifacius",
        "Bonosus", "Tiberius", "Britannicus", "Bruttidius", "Junius", "Decimus", "Burrus", "Novum", "Statius", "Epirota", "Jucundus", "Severus", "Rufus", "Caecelius", "Stabo", "Caepio", "Callistus", "Calvinus", "Corbulo", "Sabinus", "Calvus",
        "Camillus", "Crassus", "Canidius", "Caninius", "Canuleius", "Caper", "Ateius", "Capito", "Maus", "Carbo", "Arvina", "Aurelius", "Carinus", "Carrinus", "Carus", "Maximus", "Ruga", "Carvilius", "Casca", "Cassiodorus", "Hemina", "Cassius",
        "Longinus", "Parmensis", "Chaerea", "Artorius", "Castus", "Sergius", "Catilina", "Catius", "Cato", "Porcius", "Lutatius", "Catulus", "Densus", "Dexippus", "Dicletianus", "Dolabella", "Dicidius", "Duilius", "Elagabalus", "Empylus", "Ennodius",
        "Eutropius", "Exsuperantius", "Eprius", "Ena", "Fabius", "Fabricius", "Faustina", "Faventinus", "Favorinus", "Fulvius", "Flaccus", "Flaminius", "Florus", "Cornelius", "Fronto", "Fufius", "Fulvia", "Gabinius", "Galba", "Gallus", "Gargilius",
        "Gellius", "Hosidius", "Gracchus", "Haterius", "Priscus", "Herennius", "Hirtius", "Hostius", "Hyginus", "Herodes", "Helvidius", "Icilius", "Irenaeus", "Isidorus", "Isigonus", "Januarius", "Javolenus", "Jordanes", "Juba", "Justinian", "Juventius",
        "Labeo", "Labienus", "Lactantius", "Laelius",  "Laevius", "Lampadio", "Lentulus", "Licentius", "Libanus", "Lollius", "Livius", "Ligarus", "Lucceius", "Lurius", "Luxorius", "Lygdamus", "Macer", "Macrinaus", "Macrinus", "Maecenas", "Maecianus", "Maelis",
        "Maevius", "Magnus", "Majorian", "Melissus", "Memmius", "Menenius", "Merobaudes", "Messela", "Metellus", "Metella", "Milo", "Tertia", "Mucia", "Mucianus", "Musonius", "Narses", "Naevius", "Rutlius", "Nepos", "Neratius", "Nero", "Neratius", "Novius", "Numa",
        "Numerianus", "Nymphidius", "Obsequens", "Odenathus", "Ofella", "Ogulnius", "Opilius", "Opimius", "Ostarius", "Oppius", "Otho", "Pacuvius", "Palaemon", "Palfurius", "Palladius", "Palma", "Pedius", "Persius", "Petronius", "Paulus", "Piso", "Plancus", "Plautius",
        "Plotinus", "Plotius", "Pollux", "Pompeius", "Pompeianus", "Latro", "Porphyrion", "Volero", "Pupius", "Smyrnaeus", "Asinius", "Rabirus", "Regillus", "Ricimer", "Rubellius", "Rufinus", "Curtius", "Sacerdos", "Savianus", "Scipio", "Serranus", "Sulpicius",
        "Silanus", "Silius", "Stolo", "Vulso", "Volumnius", "Sedigitus", "Vindex"
    };

    //Generate - Eric Pridz
    public string Generate()
    {
        Random random = new Random();

        // Pick random name
        int randInt = random.Next(0, allNames.Length);
        string name1 = allNames[randInt].ToLower();

        // Pick random character in the name
        char randChar = name1[random.Next(0, name1.Length)];

        // Get a list of other available names that have that same character
        string[] namesAvailable = allNames.Where(s => s.ToLower().Contains(randChar) && s != name1).ToArray();

        // Get another name in that list of names
        randInt = random.Next(0, namesAvailable.Length);
        string name2 = namesAvailable[randInt].ToLower();

        // Substring name up to the selected character excluded
        int index = name1.IndexOf(randChar);
        name1 = name1.Substring(0, index);

        // Substring name from the selected character onwards
        index = name2.IndexOf(randChar);
        name2 = name2.Substring(index);

        // Add up both names to create the final name
        return name1 + name2;
    }
}

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input)
    {
        switch (input)
        {
            case null: return "";
            case "": return "";
            default:
                return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
