﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.IO;
using Bio.IO.Phylip;
using Bio.Util.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bio.Tests.IO.Phylip
{
    /// <summary>
    /// Phylip format parser.
    /// </summary>
    [TestClass]
    public class PhylipTests
    {

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static PhylipTests()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("Bio.Tests.log");
            }
        }

        /// <summary>
        /// Parse sample FASTA file 186972391.fasta and verify that it is read correctly.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PhylipParse()
        {
            string filepath = @"TestUtils\Phylip\dna.phy";
            Assert.IsTrue(File.Exists(filepath));

            IList<Dictionary<string, string>> expectedOutput = new List<Dictionary<string, string>>();

            Dictionary<string, string> expectedAlignment = new Dictionary<string, string>();
            expectedAlignment["Cow"] = "ATGGCATATCCCATACAACTAGGATTCCAAGATGCAACATCACCAATCATAGAAGAACTA"
                    + "CTTCACTTTCATGACCACACGCTAATAATTGTCTTCTTAATTAGCTCATTAGTACTTTAC"
                    + "ATTATTTCACTAATACTAACGACAAAGCTGACCCATACAAGCACGATAGATGCACAAGAA"
                    + "GTAGAGACAATCTGAACCATTCTGCCCGCCATCATCTTAATTCTAATTGCTCTTCCTTCT"
                    + "TTACGAATTCTATACATAATAGATGAAATCAATAACCCATCTCTTACAGTAAAAACCATA"
                    + "GGACATCAGTGATACTGAAGCTATGAGTATACAGATTATGAGGACTTAAGCTTCGACTCC"
                    + "TACATAATTCCAACATCAGAATTAAAGCCAGGGGAGCTACGACTATTAGAAGTCGATAAT"
                    + "CGAGTTGTACTACCAATAGAAATAACAATCCGAATGTTAGTCTCCTCTGAAGACGTATTA"
                    + "CACTCATGAGCTGTGCCCTCTCTAGGACTAAAAACAGACGCAATCCCAGGCCGTCTAAAC"
                    + "CAAACAACCCTTATATCGTCCCGTCCAGGCTTATATTACGGTCAATGCTCAGAAATTTGC"
                    + "GGGTCAAACCACAGTTTCATACCCATTGTCCTTGAGTTAGTCCCACTAAAGTACTTTGAA"
                    + "AAATGATCTGCGTCAATATTA---------------------TAA";

            expectedAlignment["Carp"] = "ATGGCACACCCAACGCAACTAGGTTTCAAGGACGCGGCCATACCCGTTATAGAGGAACTT"
                    + "CTTCACTTCCACGACCACGCATTAATAATTGTGCTCCTAATTAGCACTTTAGTTTTATAT"
                    + "ATTATTACTGCAATGGTATCAACTAAACTTACTAATAAATATATTCTAGACTCCCAAGAA"
                    + "ATCGAAATCGTATGAACCATTCTACCAGCCGTCATTTTAGTACTAATCGCCCTGCCCTCC"
                    + "CTACGCATCCTGTACCTTATAGACGAAATTAACGACCCTCACCTGACAATTAAAGCAATA"
                    + "GGACACCAATGATACTGAAGTTACGAGTATACAGACTATGAAAATCTAGGATTCGACTCC"
                    + "TATATAGTACCAACCCAAGACCTTGCCCCCGGACAATTCCGACTTCTGGAAACAGACCAC"
                    + "CGAATAGTTGTTCCAATAGAATCCCCAGTCCGTGTCCTAGTATCTGCTGAAGACGTGCTA"
                    + "CATTCTTGAGCTGTTCCATCCCTTGGCGTAAAAATGGACGCAGTCCCAGGACGACTAAAT"
                    + "CAAGCCGCCTTTATTGCCTCACGCCCAGGGGTCTTTTACGGACAATGCTCTGAAATTTGT"
                    + "GGAGCTAATCACAGCTTTATACCAATTGTAGTTGAAGCAGTACCTCTCGAACACTTCGAA"
                    + "AACTGATCCTCATTAATACTAGAAGACGCCTCGCTAGGAAGCTAA";

            expectedAlignment["Chicken"] = "ATGGCCAACCACTCCCAACTAGGCTTTCAAGACGCCTCATCCCCCATCATAGAAGAGCTC"
                    + "GTTGAATTCCACGACCACGCCCTGATAGTCGCACTAGCAATTTGCAGCTTAGTACTCTAC"
                    + "CTTCTAACTCTTATACTTATAGAAAAACTATCA---TCAAACACCGTAGATGCCCAAGAA"
                    + "GTTGAACTAATCTGAACCATCCTACCCGCTATTGTCCTAGTCCTGCTTGCCCTCCCCTCC"
                    + "CTCCAAATCCTCTACATAATAGACGAAATCGACGAACCTGATCTCACCCTAAAAGCCATC"
                    + "GGACACCAATGATACTGAACCTATGAATACACAGACTTCAAGGACCTCTCATTTGACTCC"
                    + "TACATAACCCCAACAACAGACCTCCCCCTAGGCCACTTCCGCCTACTAGAAGTCGACCAT"
                    + "CGCATTGTAATCCCCATAGAATCCCCCATTCGAGTAATCATCACCGCTGATGACGTCCTC"
                    + "CACTCATGAGCCGTACCCGCCCTCGGGGTAAAAACAGACGCAATCCCTGGACGACTAAAT"
                    + "CAAACCTCCTTCATCACCACTCGACCAGGAGTGTTTTACGGACAATGCTCAGAAATCTGC"
                    + "GGAGCTAACCACAGCTACATACCCATTGTAGTAGAGTCTACCCCCCTAAAACACTTTGAA"
                    + "GCCTGATCCTCACTA------------------CTGTCATCTTAA";

            expectedAlignment["Human"] = "ATGGCACATGCAGCGCAAGTAGGTCTACAAGACGCTACTTCCCCTATCATAGAAGAGCTT"
                    + "ATCACCTTTCATGATCACGCCCTCATAATCATTTTCCTTATCTGCTTCCTAGTCCTGTAT"
                    + "GCCCTTTTCCTAACACTCACAACAAAACTAACTAATACTAACATCTCAGACGCTCAGGAA"
                    + "ATAGAAACCGTCTGAACTATCCTGCCCGCCATCATCCTAGTCCTCATCGCCCTCCCATCC"
                    + "CTACGCATCCTTTACATAACAGACGAGGTCAACGATCCCTCCCTTACCATCAAATCAATT"
                    + "GGCCACCAATGGTACTGAACCTACGAGTACACCGACTACGGCGGACTAATCTTCAACTCC"
                    + "TACATACTTCCCCCATTATTCCTAGAACCAGGCGACCTGCGACTCCTTGACGTTGACAAT"
                    + "CGAGTAGTACTCCCGATTGAAGCCCCCATTCGTATAATAATTACATCACAAGACGTCTTG"
                    + "CACTCATGAGCTGTCCCCACATTAGGCTTAAAAACAGATGCAATTCCCGGACGTCTAAAC"
                    + "CAAACCACTTTCACCGCTACACGACCGGGGGTATACTACGGTCAATGCTCTGAAATCTGT"
                    + "GGAGCAAACCACAGTTTCATGCCCATCGTCCTAGAATTAATTCCCCTAAAAATCTTTGAA"
                    + "ATA---------------------GGGCCCGTATTTACCCTATAG";

            expectedAlignment["Loach"] = "ATGGCACATCCCACACAATTAGGATTCCAAGACGCGGCCTCACCCGTAATAGAAGAACTT"
                    + "CTTCACTTCCATGACCATGCCCTAATAATTGTATTTTTGATTAGCGCCCTAGTACTTTAT"
                    + "GTTATTATTACAACCGTCTCAACAAAACTCACTAACATATATATTTTGGACTCACAAGAA"
                    + "ATTGAAATCGTATGAACTGTGCTCCCTGCCCTAATCCTCATTTTAATCGCCCTCCCCTCA"
                    + "CTACGAATTCTATATCTTATAGACGAGATTAATGACCCCCACCTAACAATTAAGGCCATG"
                    + "GGGCACCAATGATACTGAAGCTACGAGTATACTGATTATGAAAACTTAAGTTTTGACTCC"
                    + "TACATAATCCCCACCCAGGACCTAACCCCTGGACAATTCCGGCTACTAGAGACAGACCAC"
                    + "CGAATGGTTGTTCCCATAGAATCCCCTATTCGCATTCTTGTTTCCGCCGAAGATGTACTA"
                    + "CACTCCTGGGCCCTTCCAGCCATGGGGGTAAAGATAGACGCGGTCCCAGGACGCCTTAAC"
                    + "CAAACCGCCTTTATTGCCTCCCGCCCCGGGGTATTCTATGGGCAATGCTCAGAAATCTGT"
                    + "GGAGCAAACCACAGCTTTATACCCATCGTAGTAGAAGCGGTCCCACTATCTCACTTCGAA"
                    + "AACTGGTCCACCCTTATACTAAAAGACGCCTCACTAGGAAGCTAA";

            expectedAlignment["Mouse"] = "ATGGCCTACCCATTCCAACTTGGTCTACAAGACGCCACATCCCCTATTATAGAAGAGCTA"
                    + "ATAAATTTCCATGATCACACACTAATAATTGTTTTCCTAATTAGCTCCTTAGTCCTCTAT"
                    + "ATCATCTCGCTAATATTAACAACAAAACTAACACATACAAGCACAATAGATGCACAAGAA"
                    + "GTTGAAACCATTTGAACTATTCTACCAGCTGTAATCCTTATCATAATTGCTCTCCCCTCT"
                    + "CTACGCATTCTATATATAATAGACGAAATCAACAACCCCGTATTAACCGTTAAAACCATA"
                    + "GGGCACCAATGATACTGAAGCTACGAATATACTGACTATGAAGACCTATGCTTTGATTCA"
                    + "TATATAATCCCAACAAACGACCTAAAACCTGGTGAACTACGACTGCTAGAAGTTGATAAC"
                    + "CGAGTCGTTCTGCCAATAGAACTTCCAATCCGTATATTAATTTCATCTGAAGACGTCCTC"
                    + "CACTCATGAGCAGTCCCCTCCCTAGGACTTAAAACTGATGCCATCCCAGGCCGACTAAAT"
                    + "CAAGCAACAGTAACATCAAACCGACCAGGGTTATTCTATGGCCAATGCTCTGAAATTTGT"
                    + "GGATCTAACCATAGCTTTATGCCCATTGTCCTAGAAATGGTTCCACTAAAATATTTCGAA"
                    + "AACTGATCTGCTTCAATAATT---------------------TAA";

            expectedAlignment["Rat"] = "ATGGCTTACCCATTTCAACTTGGCTTACAAGACGCTACATCACCTATCATAGAAGAACTT"
                    + "ACAAACTTTCATGACCACACCCTAATAATTGTATTCCTCATCAGCTCCCTAGTACTTTAT"
                    + "ATTATTTCACTAATACTAACAACAAAACTAACACACACAAGCACAATAGACGCCCAAGAA"
                    + "GTAGAAACAATTTGAACAATTCTCCCAGCTGTCATTCTTATTCTAATTGCCCTTCCCTCC"
                    + "CTACGAATTCTATACATAATAGACGAGATTAATAACCCAGTTCTAACAGTAAAAACTATA"
                    + "GGACACCAATGATACTGAAGCTATGAATATACTGACTATGAAGACCTATGCTTTGACTCC"
                    + "TACATAATCCCAACCAATGACCTAAAACCAGGTGAACTTCGTCTATTAGAAGTTGATAAT"
                    + "CGGGTAGTCTTACCAATAGAACTTCCAATTCGTATACTAATCTCATCCGAAGACGTCCTG"
                    + "CACTCATGAGCCATCCCTTCACTAGGGTTAAAAACCGACGCAATCCCCGGCCGCCTAAAC"
                    + "CAAGCTACAGTCACATCAAACCGACCAGGTCTATTCTATGGCCAATGCTCTGAAATTTGC"
                    + "GGCTCAAATCACAGCTTCATACCCATTGTACTAGAAATAGTGCCTCTAAAATATTTCGAA"
                    + "AACTGATCAGCTTCTATAATT---------------------TAA";

            expectedAlignment["Seal"] = "ATGGCATACCCCCTACAAATAGGCCTACAAGATGCAACCTCTCCCATTATAGAGGAGTTA"
                    + "CTACACTTCCATGACCACACATTAATAATTGTGTTCCTAATTAGCTCATTAGTACTCTAC"
                    + "ATTATCTCACTTATACTAACCACGAAACTCACCCACACAAGTACAATAGACGCACAAGAA"
                    + "GTGGAAACGGTGTGAACGATCCTACCCGCTATCATTTTAATTCTCATTGCCCTACCATCA"
                    + "TTACGAATCCTCTACATAATGGACGAGATCAATAACCCTTCCTTGACCGTAAAAACTATA"
                    + "GGACATCAGTGATACTGAAGCTATGAGTACACAGACTACGAAGACCTGAACTTTGACTCA"
                    + "TATATGATCCCCACACAAGAACTAAAGCCCGGAGAACTACGACTGCTAGAAGTAGACAAT"
                    + "CGAGTAGTCCTCCCAATAGAAATAACAATCCGCATACTAATCTCATCAGAAGATGTACTC"
                    + "CACTCATGAGCCGTACCGTCCCTAGGACTAAAAACTGATGCTATCCCAGGACGACTAAAC"
                    + "CAAACAACCCTAATAACCATACGACCAGGACTGTACTACGGTCAATGCTCAGAAATCTGT"
                    + "GGTTCAAACCACAGCTTCATACCTATTGTCCTCGAATTGGTCCCACTATCCCACTTCGAG"
                    + "AAATGATCTACCTCAATGCTT---------------------TAA";

            expectedAlignment["Whale"] = "ATGGCATATCCATTCCAACTAGGTTTCCAAGATGCAGCATCACCCATCATAGAAGAGCTC"
                    + "CTACACTTTCACGATCATACACTAATAATCGTTTTTCTAATTAGCTCTTTAGTTCTCTAC"
                    + "ATTATTACCCTAATGCTTACAACCAAATTAACACATACTAGTACAATAGACGCCCAAGAA"
                    + "GTAGAAACTGTCTGAACTATCCTCCCAGCCATTATCTTAATTTTAATTGCCTTGCCTTCA"
                    + "TTACGGATCCTTTACATAATAGACGAAGTCAATAACCCCTCCCTCACTGTAAAAACAATA"
                    + "GGTCACCAATGATATTGAAGCTATGAGTATACCGACTACGAAGACCTAAGCTTCGACTCC"
                    + "TATATAATCCCAACATCAGACCTAAAGCCAGGAGAACTACGATTATTAGAAGTAGATAAC"
                    + "CGAGTTGTCTTACCTATAGAAATAACAATCCGAATATTAGTCTCATCAGAAGACGTACTC"
                    + "CACTCATGGGCCGTACCCTCCTTGGGCCTAAAAACAGATGCAATCCCAGGACGCCTAAAC"
                    + "CAAACAACCTTAATATCAACACGACCAGGCCTATTTTATGGACAATGCTCAGAGATCTGC"
                    + "GGCTCAAACCACAGTTTCATACCAATTGTCCTAGAACTAGTACCCCTAGAAGTCTTTGAA"
                    + "AAATGATCTGTATCAATACTA---------------------TAA";

            expectedAlignment["Frog"] = "ATGGCACACCCATCACAATTAGGTTTTCAAGACGCAGCCTCTCCAATTATAGAAGAATTA"
                    + "CTTCACTTCCACGACCATACCCTCATAGCCGTTTTTCTTATTAGTACGCTAGTTCTTTAC"
                    + "ATTATTACTATTATAATAACTACTAAACTAACTAATACAAACCTAATGGACGCACAAGAG"
                    + "ATCGAAATAGTGTGAACTATTATACCAGCTATTAGCCTCATCATAATTGCCCTTCCATCC"
                    + "CTTCGTATCCTATATTTAATAGATGAAGTTAATGATCCACACTTAACAATTAAAGCAATC"
                    + "GGCCACCAATGATACTGAAGCTACGAATATACTAACTATGAGGATCTCTCATTTGACTCT"
                    + "TATATAATTCCAACTAATGACCTTACCCCTGGACAATTCCGGCTGCTAGAAGTTGATAAT"
                    + "CGAATAGTAGTCCCAATAGAATCTCCAACCCGACTTTTAGTTACAGCCGAAGACGTCCTC"
                    + "CACTCGTGAGCTGTACCCTCCTTGGGTGTCAAAACAGATGCAATCCCAGGACGACTTCAT"
                    + "CAAACATCATTTATTGCTACTCGTCCGGGAGTATTTTACGGACAATGTTCAGAAATTTGC"
                    + "GGAGCAAACCACAGCTTTATACCAATTGTAGTTGAAGCAGTACCGCTAACCGACTTTGAA"
                    + "AACTGATCTTCATCAATACTA---GAAGCATCACTA------AGA";

            expectedOutput.Add(expectedAlignment);

            IList<ISequenceAlignment> actualOutput = null;
            ISequenceAlignmentParser parser = new PhylipParser();

            using (StreamReader reader = File.OpenText(filepath))
            {
                actualOutput = parser.Parse(reader);
            }

            CompareOutput(actualOutput, expectedOutput);
        }

        /// <summary>
        /// Parse sample Phylip file dna.phy and verify that it is read correctly.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PhylipParseOne()
        {
            string filepath = @"TestUtils\Phylip\dna.phy";
            Assert.IsTrue(File.Exists(filepath));

            IList<Dictionary<string, string>> expectedOutput = new List<Dictionary<string, string>>();

            Dictionary<string, string> expectedAlignment = new Dictionary<string, string>();
            expectedAlignment["Cow"] = "ATGGCATATCCCATACAACTAGGATTCCAAGATGCAACATCACCAATCATAGAAGAACTA"
                    + "CTTCACTTTCATGACCACACGCTAATAATTGTCTTCTTAATTAGCTCATTAGTACTTTAC"
                    + "ATTATTTCACTAATACTAACGACAAAGCTGACCCATACAAGCACGATAGATGCACAAGAA"
                    + "GTAGAGACAATCTGAACCATTCTGCCCGCCATCATCTTAATTCTAATTGCTCTTCCTTCT"
                    + "TTACGAATTCTATACATAATAGATGAAATCAATAACCCATCTCTTACAGTAAAAACCATA"
                    + "GGACATCAGTGATACTGAAGCTATGAGTATACAGATTATGAGGACTTAAGCTTCGACTCC"
                    + "TACATAATTCCAACATCAGAATTAAAGCCAGGGGAGCTACGACTATTAGAAGTCGATAAT"
                    + "CGAGTTGTACTACCAATAGAAATAACAATCCGAATGTTAGTCTCCTCTGAAGACGTATTA"
                    + "CACTCATGAGCTGTGCCCTCTCTAGGACTAAAAACAGACGCAATCCCAGGCCGTCTAAAC"
                    + "CAAACAACCCTTATATCGTCCCGTCCAGGCTTATATTACGGTCAATGCTCAGAAATTTGC"
                    + "GGGTCAAACCACAGTTTCATACCCATTGTCCTTGAGTTAGTCCCACTAAAGTACTTTGAA"
                    + "AAATGATCTGCGTCAATATTA---------------------TAA";

            expectedAlignment["Carp"] = "ATGGCACACCCAACGCAACTAGGTTTCAAGGACGCGGCCATACCCGTTATAGAGGAACTT"
                    + "CTTCACTTCCACGACCACGCATTAATAATTGTGCTCCTAATTAGCACTTTAGTTTTATAT"
                    + "ATTATTACTGCAATGGTATCAACTAAACTTACTAATAAATATATTCTAGACTCCCAAGAA"
                    + "ATCGAAATCGTATGAACCATTCTACCAGCCGTCATTTTAGTACTAATCGCCCTGCCCTCC"
                    + "CTACGCATCCTGTACCTTATAGACGAAATTAACGACCCTCACCTGACAATTAAAGCAATA"
                    + "GGACACCAATGATACTGAAGTTACGAGTATACAGACTATGAAAATCTAGGATTCGACTCC"
                    + "TATATAGTACCAACCCAAGACCTTGCCCCCGGACAATTCCGACTTCTGGAAACAGACCAC"
                    + "CGAATAGTTGTTCCAATAGAATCCCCAGTCCGTGTCCTAGTATCTGCTGAAGACGTGCTA"
                    + "CATTCTTGAGCTGTTCCATCCCTTGGCGTAAAAATGGACGCAGTCCCAGGACGACTAAAT"
                    + "CAAGCCGCCTTTATTGCCTCACGCCCAGGGGTCTTTTACGGACAATGCTCTGAAATTTGT"
                    + "GGAGCTAATCACAGCTTTATACCAATTGTAGTTGAAGCAGTACCTCTCGAACACTTCGAA"
                    + "AACTGATCCTCATTAATACTAGAAGACGCCTCGCTAGGAAGCTAA";

            expectedAlignment["Chicken"] = "ATGGCCAACCACTCCCAACTAGGCTTTCAAGACGCCTCATCCCCCATCATAGAAGAGCTC"
                    + "GTTGAATTCCACGACCACGCCCTGATAGTCGCACTAGCAATTTGCAGCTTAGTACTCTAC"
                    + "CTTCTAACTCTTATACTTATAGAAAAACTATCA---TCAAACACCGTAGATGCCCAAGAA"
                    + "GTTGAACTAATCTGAACCATCCTACCCGCTATTGTCCTAGTCCTGCTTGCCCTCCCCTCC"
                    + "CTCCAAATCCTCTACATAATAGACGAAATCGACGAACCTGATCTCACCCTAAAAGCCATC"
                    + "GGACACCAATGATACTGAACCTATGAATACACAGACTTCAAGGACCTCTCATTTGACTCC"
                    + "TACATAACCCCAACAACAGACCTCCCCCTAGGCCACTTCCGCCTACTAGAAGTCGACCAT"
                    + "CGCATTGTAATCCCCATAGAATCCCCCATTCGAGTAATCATCACCGCTGATGACGTCCTC"
                    + "CACTCATGAGCCGTACCCGCCCTCGGGGTAAAAACAGACGCAATCCCTGGACGACTAAAT"
                    + "CAAACCTCCTTCATCACCACTCGACCAGGAGTGTTTTACGGACAATGCTCAGAAATCTGC"
                    + "GGAGCTAACCACAGCTACATACCCATTGTAGTAGAGTCTACCCCCCTAAAACACTTTGAA"
                    + "GCCTGATCCTCACTA------------------CTGTCATCTTAA";

            expectedAlignment["Human"] = "ATGGCACATGCAGCGCAAGTAGGTCTACAAGACGCTACTTCCCCTATCATAGAAGAGCTT"
                    + "ATCACCTTTCATGATCACGCCCTCATAATCATTTTCCTTATCTGCTTCCTAGTCCTGTAT"
                    + "GCCCTTTTCCTAACACTCACAACAAAACTAACTAATACTAACATCTCAGACGCTCAGGAA"
                    + "ATAGAAACCGTCTGAACTATCCTGCCCGCCATCATCCTAGTCCTCATCGCCCTCCCATCC"
                    + "CTACGCATCCTTTACATAACAGACGAGGTCAACGATCCCTCCCTTACCATCAAATCAATT"
                    + "GGCCACCAATGGTACTGAACCTACGAGTACACCGACTACGGCGGACTAATCTTCAACTCC"
                    + "TACATACTTCCCCCATTATTCCTAGAACCAGGCGACCTGCGACTCCTTGACGTTGACAAT"
                    + "CGAGTAGTACTCCCGATTGAAGCCCCCATTCGTATAATAATTACATCACAAGACGTCTTG"
                    + "CACTCATGAGCTGTCCCCACATTAGGCTTAAAAACAGATGCAATTCCCGGACGTCTAAAC"
                    + "CAAACCACTTTCACCGCTACACGACCGGGGGTATACTACGGTCAATGCTCTGAAATCTGT"
                    + "GGAGCAAACCACAGTTTCATGCCCATCGTCCTAGAATTAATTCCCCTAAAAATCTTTGAA"
                    + "ATA---------------------GGGCCCGTATTTACCCTATAG";

            expectedAlignment["Loach"] = "ATGGCACATCCCACACAATTAGGATTCCAAGACGCGGCCTCACCCGTAATAGAAGAACTT"
                    + "CTTCACTTCCATGACCATGCCCTAATAATTGTATTTTTGATTAGCGCCCTAGTACTTTAT"
                    + "GTTATTATTACAACCGTCTCAACAAAACTCACTAACATATATATTTTGGACTCACAAGAA"
                    + "ATTGAAATCGTATGAACTGTGCTCCCTGCCCTAATCCTCATTTTAATCGCCCTCCCCTCA"
                    + "CTACGAATTCTATATCTTATAGACGAGATTAATGACCCCCACCTAACAATTAAGGCCATG"
                    + "GGGCACCAATGATACTGAAGCTACGAGTATACTGATTATGAAAACTTAAGTTTTGACTCC"
                    + "TACATAATCCCCACCCAGGACCTAACCCCTGGACAATTCCGGCTACTAGAGACAGACCAC"
                    + "CGAATGGTTGTTCCCATAGAATCCCCTATTCGCATTCTTGTTTCCGCCGAAGATGTACTA"
                    + "CACTCCTGGGCCCTTCCAGCCATGGGGGTAAAGATAGACGCGGTCCCAGGACGCCTTAAC"
                    + "CAAACCGCCTTTATTGCCTCCCGCCCCGGGGTATTCTATGGGCAATGCTCAGAAATCTGT"
                    + "GGAGCAAACCACAGCTTTATACCCATCGTAGTAGAAGCGGTCCCACTATCTCACTTCGAA"
                    + "AACTGGTCCACCCTTATACTAAAAGACGCCTCACTAGGAAGCTAA";

            expectedAlignment["Mouse"] = "ATGGCCTACCCATTCCAACTTGGTCTACAAGACGCCACATCCCCTATTATAGAAGAGCTA"
                    + "ATAAATTTCCATGATCACACACTAATAATTGTTTTCCTAATTAGCTCCTTAGTCCTCTAT"
                    + "ATCATCTCGCTAATATTAACAACAAAACTAACACATACAAGCACAATAGATGCACAAGAA"
                    + "GTTGAAACCATTTGAACTATTCTACCAGCTGTAATCCTTATCATAATTGCTCTCCCCTCT"
                    + "CTACGCATTCTATATATAATAGACGAAATCAACAACCCCGTATTAACCGTTAAAACCATA"
                    + "GGGCACCAATGATACTGAAGCTACGAATATACTGACTATGAAGACCTATGCTTTGATTCA"
                    + "TATATAATCCCAACAAACGACCTAAAACCTGGTGAACTACGACTGCTAGAAGTTGATAAC"
                    + "CGAGTCGTTCTGCCAATAGAACTTCCAATCCGTATATTAATTTCATCTGAAGACGTCCTC"
                    + "CACTCATGAGCAGTCCCCTCCCTAGGACTTAAAACTGATGCCATCCCAGGCCGACTAAAT"
                    + "CAAGCAACAGTAACATCAAACCGACCAGGGTTATTCTATGGCCAATGCTCTGAAATTTGT"
                    + "GGATCTAACCATAGCTTTATGCCCATTGTCCTAGAAATGGTTCCACTAAAATATTTCGAA"
                    + "AACTGATCTGCTTCAATAATT---------------------TAA";

            expectedAlignment["Rat"] = "ATGGCTTACCCATTTCAACTTGGCTTACAAGACGCTACATCACCTATCATAGAAGAACTT"
                    + "ACAAACTTTCATGACCACACCCTAATAATTGTATTCCTCATCAGCTCCCTAGTACTTTAT"
                    + "ATTATTTCACTAATACTAACAACAAAACTAACACACACAAGCACAATAGACGCCCAAGAA"
                    + "GTAGAAACAATTTGAACAATTCTCCCAGCTGTCATTCTTATTCTAATTGCCCTTCCCTCC"
                    + "CTACGAATTCTATACATAATAGACGAGATTAATAACCCAGTTCTAACAGTAAAAACTATA"
                    + "GGACACCAATGATACTGAAGCTATGAATATACTGACTATGAAGACCTATGCTTTGACTCC"
                    + "TACATAATCCCAACCAATGACCTAAAACCAGGTGAACTTCGTCTATTAGAAGTTGATAAT"
                    + "CGGGTAGTCTTACCAATAGAACTTCCAATTCGTATACTAATCTCATCCGAAGACGTCCTG"
                    + "CACTCATGAGCCATCCCTTCACTAGGGTTAAAAACCGACGCAATCCCCGGCCGCCTAAAC"
                    + "CAAGCTACAGTCACATCAAACCGACCAGGTCTATTCTATGGCCAATGCTCTGAAATTTGC"
                    + "GGCTCAAATCACAGCTTCATACCCATTGTACTAGAAATAGTGCCTCTAAAATATTTCGAA"
                    + "AACTGATCAGCTTCTATAATT---------------------TAA";

            expectedAlignment["Seal"] = "ATGGCATACCCCCTACAAATAGGCCTACAAGATGCAACCTCTCCCATTATAGAGGAGTTA"
                    + "CTACACTTCCATGACCACACATTAATAATTGTGTTCCTAATTAGCTCATTAGTACTCTAC"
                    + "ATTATCTCACTTATACTAACCACGAAACTCACCCACACAAGTACAATAGACGCACAAGAA"
                    + "GTGGAAACGGTGTGAACGATCCTACCCGCTATCATTTTAATTCTCATTGCCCTACCATCA"
                    + "TTACGAATCCTCTACATAATGGACGAGATCAATAACCCTTCCTTGACCGTAAAAACTATA"
                    + "GGACATCAGTGATACTGAAGCTATGAGTACACAGACTACGAAGACCTGAACTTTGACTCA"
                    + "TATATGATCCCCACACAAGAACTAAAGCCCGGAGAACTACGACTGCTAGAAGTAGACAAT"
                    + "CGAGTAGTCCTCCCAATAGAAATAACAATCCGCATACTAATCTCATCAGAAGATGTACTC"
                    + "CACTCATGAGCCGTACCGTCCCTAGGACTAAAAACTGATGCTATCCCAGGACGACTAAAC"
                    + "CAAACAACCCTAATAACCATACGACCAGGACTGTACTACGGTCAATGCTCAGAAATCTGT"
                    + "GGTTCAAACCACAGCTTCATACCTATTGTCCTCGAATTGGTCCCACTATCCCACTTCGAG"
                    + "AAATGATCTACCTCAATGCTT---------------------TAA";

            expectedAlignment["Whale"] = "ATGGCATATCCATTCCAACTAGGTTTCCAAGATGCAGCATCACCCATCATAGAAGAGCTC"
                    + "CTACACTTTCACGATCATACACTAATAATCGTTTTTCTAATTAGCTCTTTAGTTCTCTAC"
                    + "ATTATTACCCTAATGCTTACAACCAAATTAACACATACTAGTACAATAGACGCCCAAGAA"
                    + "GTAGAAACTGTCTGAACTATCCTCCCAGCCATTATCTTAATTTTAATTGCCTTGCCTTCA"
                    + "TTACGGATCCTTTACATAATAGACGAAGTCAATAACCCCTCCCTCACTGTAAAAACAATA"
                    + "GGTCACCAATGATATTGAAGCTATGAGTATACCGACTACGAAGACCTAAGCTTCGACTCC"
                    + "TATATAATCCCAACATCAGACCTAAAGCCAGGAGAACTACGATTATTAGAAGTAGATAAC"
                    + "CGAGTTGTCTTACCTATAGAAATAACAATCCGAATATTAGTCTCATCAGAAGACGTACTC"
                    + "CACTCATGGGCCGTACCCTCCTTGGGCCTAAAAACAGATGCAATCCCAGGACGCCTAAAC"
                    + "CAAACAACCTTAATATCAACACGACCAGGCCTATTTTATGGACAATGCTCAGAGATCTGC"
                    + "GGCTCAAACCACAGTTTCATACCAATTGTCCTAGAACTAGTACCCCTAGAAGTCTTTGAA"
                    + "AAATGATCTGTATCAATACTA---------------------TAA";

            expectedAlignment["Frog"] = "ATGGCACACCCATCACAATTAGGTTTTCAAGACGCAGCCTCTCCAATTATAGAAGAATTA"
                    + "CTTCACTTCCACGACCATACCCTCATAGCCGTTTTTCTTATTAGTACGCTAGTTCTTTAC"
                    + "ATTATTACTATTATAATAACTACTAAACTAACTAATACAAACCTAATGGACGCACAAGAG"
                    + "ATCGAAATAGTGTGAACTATTATACCAGCTATTAGCCTCATCATAATTGCCCTTCCATCC"
                    + "CTTCGTATCCTATATTTAATAGATGAAGTTAATGATCCACACTTAACAATTAAAGCAATC"
                    + "GGCCACCAATGATACTGAAGCTACGAATATACTAACTATGAGGATCTCTCATTTGACTCT"
                    + "TATATAATTCCAACTAATGACCTTACCCCTGGACAATTCCGGCTGCTAGAAGTTGATAAT"
                    + "CGAATAGTAGTCCCAATAGAATCTCCAACCCGACTTTTAGTTACAGCCGAAGACGTCCTC"
                    + "CACTCGTGAGCTGTACCCTCCTTGGGTGTCAAAACAGATGCAATCCCAGGACGACTTCAT"
                    + "CAAACATCATTTATTGCTACTCGTCCGGGAGTATTTTACGGACAATGTTCAGAAATTTGC"
                    + "GGAGCAAACCACAGCTTTATACCAATTGTAGTTGAAGCAGTACCGCTAACCGACTTTGAA"
                    + "AACTGATCTTCATCAATACTA---GAAGCATCACTA------AGA";

            expectedOutput.Add(expectedAlignment);

            List<ISequenceAlignment> actualOutput = new List<ISequenceAlignment>();
            ISequenceAlignment actualAlignment = null;
            ISequenceAlignmentParser parser = new PhylipParser();

            using (StreamReader reader = File.OpenText(filepath))
            {
                actualAlignment = parser.ParseOne(reader);
            }
            actualOutput.Add(actualAlignment);
            CompareOutput(actualOutput, expectedOutput);
        }

        /// <summary>
        /// Parse sample Phylip file primates.phy and verify that it is read correctly.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PhylipParsePrimates()
        {
            string filepath = @"TestUtils\Phylip\primates.phy";
            Assert.IsTrue(File.Exists(filepath));

            IList<Dictionary<string, string>> expectedOutput = new List<Dictionary<string, string>>();

            Dictionary<string, string> expectedAlignment = new Dictionary<string, string>();
            expectedAlignment["Mouse"] = "ACCAAAAAAACATCCAAACACCAACCCCAGCCCTTACGCAATAGCCATACAAAGAATATT"
                    + "ATACTACTAAAAACTCAAATTAACTCTTTAATCTTTATACAACATTCCACCAACCTATCC"
                    + "ACACAAAAAAACTCATATTTATCTAAATACGAACTTCACACAACCTTAACACATAAACAT"
                    + "ACCCCAGCCCAACACCCTTCCACAAATCCTTAATATACGCACCATAAATAAC";

            expectedAlignment["Bovine"] = "ACCAAACCTGTCCCCACCATCTAACACCAACCCACATATACAAGCTAAACCAAAAATACC"
                    + "ATACAACCATAAATAAGACTAATCTATTAAAATAACCCATTACGATACAAAATCCCTTTC"
                    + "GTCTAGATACAAACCACAACACACAATTAATACACACCACAATTACAATACTAAACTCCC"
                    + "ATCCCACCAAATCACCCTCCATCAAATCCACAAATTACACAACCATTAACCC";

            expectedAlignment["Lemur"] = "ACCAAACTAACATCTAACAACTACCTCCAACTCTAAAAAAGCACTCTTACCAAACCCATC"
                    + "ACAACTCTATCAACCTAACCAAACTATCAACATGCCCTCTCCTAATTAAAAACATTGCCA"
                    + "CACTAAACCTACACACCTCATCACCATTAACGCATAACTCCTCAGTCATATCTACTACAC"
                    + "ACCCTAACAATTTATCCCTCCCATAATCCAAAAACTCCATAAACACAAATTC";

            expectedAlignment["Tarsier"] = "ATCTACCTTATCTCCCCCAATCAATACCAACCTAAAAACTCTACAATTAAAAACCCCACC"
                    + "GCTCAATTACTAGCAAAAATAGACATTCAACTCCTCCCATCATAACATAAAACATTCCTC"
                    + "GCTCCAATAAACACATCACAATCCCAATAACGCATATACCTAAATACATCATTTAATAAT"
                    + "AATACTCCAACTCCCATAACACAGCATACATAAACTCCATAAGTTTGAACAC";

            expectedAlignment["Squir Monk"] = "ACCCCAGCAACTCGTTGTGACCAACATCAATCCAAAATTAGCAAACGTACCAACAATCTC"
                    + "CCAAATTTAAAAACACATCCTACCTTTACAATTAATAACCATTGTCTAGATATACCCCTA"
                    + "AAATAAATGAATATAAACCCTCGCCGATAACATA-ACCCCTAAAATCAAGACATCCTCTC"
                    + "ACAACGCCAAACCCCCCTCTCATAACTCTACAAAATACACAATCACCAACAC";

            expectedAlignment["Jpn Macaq"] = "ACTCCACCTGCTCACCTCATCCACTACTACTCCTCAAGCAATACATAAACTAAAAACTTC"
                    + "TCACCTCTAATACTACACACCACTCCTGAAATCAATGCCCTCCACTAAAAAACATCACCA"
                    + "GCCCAAACAAACACCTATCTACCCCCCCGGTCCACGCCCCTAACTCCATCATTCCCCCTC"
                    + "AATACATCAAACAATTCCCCCCAATACCCACAAACTGCATAAGCAAACAGAC";

            expectedAlignment["Rhesus Mac"] = "ACTTCACCCGTTCACCTCATCCACTACTACTCCTCAAGCGATACATAAATCAAAAACTTC"
                    + "TCACCTCCAATACTACGCACCGCTCCTAAAATCAATGCCCCCCACCAAAAAACATCACCA"
                    + "ACCCAAACAAACACCTACCCATCCCCCCGGTTCACGCCTCAAACTCCATCATTCCCCCTC"
                    + "AATACATCAAACAATTCCCCCCAATACCCACAAACTACATAAACAAACAAAC";

            expectedAlignment["Crab-E.Mac"] = "ACCCCACCTACCCGCCTCGTCCGCTACTGCTTCTCAAACAATATATAGACCAACAACTTC"
                    + "TCACCTTTAACACTACATATCACTCCTGAGCTTAACACCCTCCGCTAAAAAACACCACTA"
                    + "ACCCAAACAAACACCTATCTATCCCCCCGGTCCACGCCCCAAACCCCGCTATTCCCCCCT"
                    + "AATACACCAAACAATTTTCTCCAACACCCACAAACTGTATAAACAAACAAAC";

            expectedAlignment["BarbMacaq"] = "ACCCTATCTATCTACCTCACCCGCCACCACCCCCCAAACAACACACAAACCAACAACTTT"
                    + "TTATCTTTAGCACCACACATCACCCCCAAAAGCAATACCCTTCACCAAAAAGCACCATCA"
                    + "AATCAAACAAACACCTATTTATTCCCCTAATTCACGTCCCAAATCCCATTATCTCTCCCC"
                    + "AACATACCAAACAATTCTCCCTAATATACACAAACCACGCAAACAAACAAAC";

            expectedAlignment["Gibbon"] = "ACTATACCCACCCAACTCGACCTACACCAATCCCCACATAGCACACAGACCAACAACCTC"
                    + "CCACCTTCCATACCAAGCCCCGACTTTACCGCCAACGCACCTCATCAAAACATACCTACA"
                    + "ACACAAACAAATGCCCCCCCACCCTCCTTCTTCAAGCCCACTAGACCATCCTACCTTCCT"
                    + "AGCACGCCAAGCTCTCTACCATCAAACGCACAACTTACACATACAGAACCAC";

            expectedAlignment["Orang"] = "ACCCCACCCGTCTACACCAGCCAACACCAACCCCCACCTACTATACCAACCAATAACCTC"
                    + "TCAACCCCTAAACCAAACACTATCCCCAAAACCAACACACTCTACCAAAATACACCCCCA"
                    + "ATTCACATCCGCACACCCCCACCCCCCCTGCCCACGTCCATCCCATCACCCTCTCCTCCC"
                    + "AACACCCTAAGCCACCTTCCTCAAAATCCAAAACCCACACAACCGAAACAAC";

            expectedAlignment["Gorilla"] = "ACCCCATTTATCCATAAAAACCAACACCAACCCCCATCTAACACACAAACTAATGACCCC"
                    + "CCACCCTCAAAGCCAAACACCAACCCTATAATCAATACGCCTTATCAAAACACACCCCCA"
                    + "ACATAAACCCACGCACCCCCACCCCTTCCGCCCATGCTCACCACATCATCTCTCCCCTTC"
                    + "AACACCTCAATCCACCTCCCCCCAAATACACAATTCACACAAACAATACCAC";

            expectedAlignment["Chimp"] = "ACCCCATCCACCCATACAAACCAACATTACCCTCCATCCAATATACAAACTAACAACCTC"
                    + "CCACTCTTCAGACCGAACACCAATCTCACAACCAACACGCCCCGTCAAAACACCCCTTCA"
                    + "GCACAAATTCATACACCCCTACCTTTCCTACCCACGTTCACCACATCATCCCCCCCTCTC"
                    + "AACATCTTGACTCGCCTCTCTCCAAACACACAATTCACGCAAACAACGCCAC";

            expectedAlignment["Human"] = "ACCCCACTCACCCATACAAACCAACACCACTCTCCACCTAATATACAAATTAATAACCTC"
                    + "CCACCTTCAGAACTGAACGCCAATCTCATAACCAACACACCCCATCAAAGCACCCCTCCA"
                    + "ACACAAACCCGCACACCTCCACCCCCCTCGTCTACGCTTACCACGTCATCCCTCCCTCTC"
                    + "AACACCTTAACTCACCTTCTCCCAAACGCACAATTCGCACACACAACGCCAC";

            expectedOutput.Add(expectedAlignment);

            IList<ISequenceAlignment> actualOutput = null;
            ISequenceAlignmentParser parser = new PhylipParser();

            using (StreamReader reader = File.OpenText(filepath))
            {
                actualOutput = parser.Parse(reader);
            }

            CompareOutput(actualOutput, expectedOutput);
        }

        /// <summary>
        /// Compare the actual output to expected output
        /// </summary>
        /// <param name="actualOutput">Actual output</param>
        /// <param name="expectedOutput">Expected output</param>
        /// <returns></returns>
        private static bool CompareOutput(
                IList<ISequenceAlignment> actualOutput,
                IList<Dictionary<string, string>> expectedOutput)
        {
            if (actualOutput.Count != expectedOutput.Count)
            {
                return false;
            }

            int alignmentIndex = 0;
            foreach (ISequenceAlignment alignment in actualOutput)
            {
                Dictionary<string, string> expcetedAlignment = expectedOutput[alignmentIndex];

                foreach (Sequence actualSequence in alignment.AlignedSequences[0].Sequences)
                {
                    if (0 != string.Compare(new string(actualSequence.Select(a => (char)a).ToArray()),
                            expcetedAlignment[actualSequence.ID],
                            true, CultureInfo.CurrentCulture))
                    {
                        return false;
                    }
                }

                alignmentIndex++;
            }

            return true;
        }
    }
}
