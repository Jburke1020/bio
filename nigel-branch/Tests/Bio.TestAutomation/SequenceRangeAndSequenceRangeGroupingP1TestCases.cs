﻿/****************************************************************************
 * SequenceRangeAndSequenceRangeGroupingP1TestCases.cs * 
 * This file contains the SequenceRange, SequenceRangeGrouping 
 * operations P1 test cases.* 
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Bio.IO.Bed;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if (SILVERLIGHT == false)

namespace Bio.TestAutomation
#else
    namespace Bio.Silverlight.TestAutomation
#endif
{
    /// <summary>
    ///     Test Automation code for BIO SequenceRange operations P1 level validations.
    /// </summary>
    [TestClass]
    public class SequenceRangeAndSequenceRangeGroupingP1TestCases
    {
        #region Enums

        /// <summary>
        ///     BED Operations parameters.
        /// </summary>
        private enum BedOperationsParameters
        {
            Merge,
            MergeQueryWithReference,
            MergeWithPam,
            Intersect,
            Subtract,
        };

        #endregion Enums

        #region Global Variables

        private readonly Utility utilityObj = new Utility(@"TestUtils\BedTestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        ///     Static constructor to open log and make other settings needed for test
        /// </summary>
        static SequenceRangeAndSequenceRangeGroupingP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("Bio.automation.log");
            }
        }

        #endregion Constructor

        #region SequenceRange and SequenceRangeGrouping P1 TestCases

        /// <summary>
        ///     Validate Compare two sequences.
        ///     Input Data : Valid Range ID,Start and End.
        ///     Output Data : Validation of comparing two sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateCompareSequenceRangeWithIdenticalStartIndex()
        {
            ValidateCompareTwoSequenceRanges(Constants.CompareSequenceRangeWithIdenticalStartNode);
        }

        /// <summary>
        ///     Validate Compare two sequences with Identical ENDs.
        ///     Input Data : Valid Range ID,Start and End.
        ///     Output Data : Validation of comparing two sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateCompareSequenceRangeWithIdenticalENDIndex()
        {
            ValidateCompareTwoSequenceRanges(Constants.CompareSequenceRangeWithIdenticalENDNode);
        }

        /// <summary>
        ///     Validate Merge sequenceRange with identical chromosome entries.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMergeWithIdenticalChromoEntries()
        {
            ValidateBedOperations(Constants.MergesmallFilewithIdenticalChromosomesNode,
                                  BedOperationsParameters.Merge, false, true);
        }

        /// <summary>
        ///     Validate Merge sequenceRange with All identical chromosome entries.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMergeWithAllIdenticalChromoEntries()
        {
            ValidateBedOperations(Constants.MergeFilewithAllIdenticalChromosomesNode,
                                  BedOperationsParameters.Merge, false, false);
        }

        /// <summary>
        ///     Validate Merge Two small size Bed files
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMergeTwoBedFiles()
        {
            ValidateBedOperations(Constants.MergeTwosmallFilesNode,
                                  BedOperationsParameters.MergeQueryWithReference, false, false);
        }

        /// <summary>
        ///     Validate Merge Two small size Bed files with Identical Chromosome entries.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMergeTwoBedFilesWithTwoIdenticalChromo()
        {
            ValidateBedOperations(Constants.MergeTwoFileswithAllIdenticalChromosomesNode,
                                  BedOperationsParameters.MergeWithPam, false, true);
        }

        /// <summary>
        ///     Validate Intersect Two small size Bed files without pieces of intervals..
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectTwoBedFilesWithoutIntervals()
        {
            ValidateBedOperations(Constants.IntersectWithIdenticalChromoWithoutIntervals,
                                  BedOperationsParameters.Intersect, false, true);
        }

        /// <summary>
        ///     Validate Intersect Two small size Bed files without pieces of intervals.
        ///     for all identical entries.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectTwoBedFilesWithoutIntervalsForIdenticalEntries()
        {
            ValidateBedOperations(Constants.IntersectWithAllIdenticalChromoWithoutIntervals,
                                  BedOperationsParameters.Intersect, false, false);
        }

        /// <summary>
        ///     Validate Intersect Two small size Bed files with pieces of intervals..
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectTwoBedFilesWithPiecesIntervals()
        {
            ValidateBedOperations(Constants.IntersectWithIdenticalChromoWithoutIntervals,
                                  BedOperationsParameters.Intersect, true, false);
        }

        /// <summary>
        ///     Validate Intersect Two small size Bed files with pieces of intervals.
        ///     for all identical entries.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectTwoBedFilesWithIntervalsForIdenticalEntries()
        {
            ValidateBedOperations(Constants.IntersectWithAllIdenticalChromoWithoutIntervals,
                                  BedOperationsParameters.Intersect, true, true);
        }

        /// <summary>
        ///     Validate Intersect With more than ten chromosome without pieces of intervals.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectTenChromoWithoutPiecesIntervals()
        {
            ValidateBedOperations(Constants.IntersectBedFilesWithTenChromo,
                                  BedOperationsParameters.Intersect, false, true);
        }

        /// <summary>
        ///     Validate Intersect With more than ten chromosome with pieces of intervals.
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectTenChromoWithPiecesIntervals()
        {
            ValidateBedOperations(Constants.IntersectBedFilesWithTenChromo,
                                  BedOperationsParameters.Intersect, true, false);
        }

        /// <summary>
        ///     Validate Intersect With more than ten chromosome with minimal overlap.
        ///     Input Data : Valid BED file and minimal overlap
        ///     Output Data : Validation of Merge operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIntersectBedFilesWithMinimalOverlap()
        {
            ValidateBedOperations(Constants.IntersectBedFilesWithTenChromoWithMinimalOverlap,
                                  BedOperationsParameters.Intersect, false, true);
        }

        /// <summary>
        ///     Validate subtract two small size Bed files with minimal overlap and
        ///     with non overlapping pieces of intervals
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of subtract operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubtractTwoBedFilesWithMinimalandNonOverlap()
        {
            ValidateBedOperations(Constants.SubtractBedFilesWithMinimalOverlapNodeName,
                                  BedOperationsParameters.Subtract, false, false);
        }

        /// <summary>
        ///     Validate subtract two small size Bed files and
        ///     with non overlapping pieces of intervals
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of subtract operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubtractTwoBedFilesWithNonOverlapIntervals()
        {
            ValidateBedOperations(Constants.SubtractBedFilesNodeName,
                                  BedOperationsParameters.Subtract, false, false);
        }

        /// <summary>
        ///     Validate subtract two Bed files which contains multiple chromosomes and
        ///     with non overlapping pieces of intervals
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of subtract operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubtractMultipleChromosomesWithNonOverlapIntervals()
        {
            ValidateBedOperations(Constants.SubtractMultipleChromosomesBedFilesNodeName,
                                  BedOperationsParameters.Subtract, false, false);
        }

        /// <summary>
        ///     Validate subtract of Bed files contains multiple chromosomes and
        ///     intervals with no overlap
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of subtract operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubtractMultipleChromosomesUsingIntervalsWithNoOverlap()
        {
            ValidateBedOperations(Constants.SubtractMultipleChromosomesWithIntervalsNodeName,
                                  BedOperationsParameters.Subtract, true, false);
        }

        /// <summary>
        ///     Validate subtract two small size Bed files and
        ///     intervals with no overlap
        ///     Input Data : Valid BED file.
        ///     Output Data : Validation of subtract operation.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubtractTwoBedFilesWithNonOverlappingIntervals()
        {
            ValidateBedOperations(Constants.SubtractBedFilesWithIntervalsNodeName,
                                  BedOperationsParameters.Subtract, false, true);
        }

        #endregion SequenceRange and SequenceRangeGrouping P1 TestCases

        #region Helper Methods

        /// <summary>
        ///     Validate BED Operations(Merge,Intersect)..
        /// </summary>
        /// <param name="nodeName">Xml Node name for different inputs.</param>
        /// <param name="operationPam">Different Bed operations.</param>
        /// <param name="overlappingBasePair">overlapping base pair</param>
        /// <param name="isParentSeqRangeRequired">Is Parent Sequence Range required?</param>
        private void ValidateBedOperations(string nodeName,
                                           BedOperationsParameters operationPam,
                                           bool overlappingBasePair, bool isParentSeqRangeRequired)
        {
            // Get values from xml.
            string expectedRangeIDs = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.IDNode);
            string expectedStartIndex = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.StartNode);
            string expectedEndIndex = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.EndNode);
            string referenceFilePath = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.FilePathNode);
            string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.QueryFilePath);
            string minimalOverlap = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.OverlapValue);

            SequenceRangeGrouping operationResult = null;

            // Parse a BED file.
            var parserObj = new BedParser();
            SequenceRangeGrouping referenceGroup = parserObj.ParseRangeGrouping(referenceFilePath);
            SequenceRangeGrouping queryGroup = parserObj.ParseRangeGrouping(queryFilePath);

            var intersectOutputType = IntersectOutputType.OverlappingIntervals;
            if (overlappingBasePair)
            {
                intersectOutputType = IntersectOutputType.OverlappingPiecesOfIntervals;
            }

            var subtractOutputType = SubtractOutputType.NonOverlappingPiecesOfIntervals;
            if (overlappingBasePair)
            {
                subtractOutputType = SubtractOutputType.IntervalsWithNoOverlap;
            }

            switch (operationPam)
            {
                case BedOperationsParameters.Merge:
                    operationResult = referenceGroup.MergeOverlaps();
                    break;
                case BedOperationsParameters.MergeWithPam:
                    operationResult = referenceGroup.MergeOverlaps(queryGroup,
                                                                   0, isParentSeqRangeRequired);
                    break;
                case BedOperationsParameters.Intersect:

                    operationResult = referenceGroup.Intersect(queryGroup,
                                                               long.Parse(minimalOverlap, null), intersectOutputType,
                                                               isParentSeqRangeRequired);
                    break;
                case BedOperationsParameters.MergeQueryWithReference:
                    operationResult = queryGroup.MergeOverlaps(referenceGroup,
                                                               0, isParentSeqRangeRequired);
                    break;
                case BedOperationsParameters.Subtract:
                    operationResult = referenceGroup.Subtract(queryGroup,
                                                              long.Parse(minimalOverlap, null), subtractOutputType,
                                                              isParentSeqRangeRequired);
                    break;
                default:
                    break;
            }

            // Get a result SequenceGroup Id.
            IEnumerable<string> groupId = operationResult.GroupIDs;
            string[] expectedRangeIdsArray = expectedRangeIDs.Split(',');
            string[] expectedStartIndexArray = expectedStartIndex.Split(',');
            string[] expectedEndIndexArray = expectedEndIndex.Split(',');
            int i = 0;

            foreach (string grpId in groupId)
            {
                string rangeId = grpId;

                List<ISequenceRange> rangeList = operationResult.GetGroup(rangeId);

                // Validate result sequence range.
                foreach (ISequenceRange range in rangeList)
                {
                    Assert.AreEqual(expectedRangeIdsArray[i], range.ID);
                    Assert.AreEqual(expectedStartIndexArray[i], range.Start.ToString((IFormatProvider) null));
                    Assert.AreEqual(expectedEndIndexArray[i], range.End.ToString((IFormatProvider) null));
                    i++;
                }
            }

            // Validate ParentSeqRange.
            bool result = ValidateParentSeqRange(operationResult, referenceGroup, queryGroup, isParentSeqRangeRequired);
            Assert.IsTrue(result);

            ApplicationLog.WriteLine("Bed Operations BVT: Successfully validated the BED SequenceID, Start and End Ranges");
        }

        /// <summary>
        ///     Compare two sequence ranges.
        /// </summary>
        /// <param name="nodeName">Xml Node name for different inputs.</param>
        private void ValidateCompareTwoSequenceRanges(string nodeName)
        {
            // Get values from xml.
            string expectedRangeId = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.IDNode);
            string expectedStartIndex = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.StartNode);
            string expectedEndIndex = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.EndNode);
            string expectedRangeId1 = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.IDNode1);
            string expectedStartIndex1 = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.StartNode1);
            string expectedEndIndex1 = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.EndNode1);
            string expectedResults = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.ComparisonResult);

            // Create first SequenceRange.
            var seqRange = new SequenceRange(expectedRangeId, long.Parse(expectedStartIndex, null), long.Parse(expectedEndIndex, null));

            // Create second SequenceRange.
            var secondSeqRange = new SequenceRange(expectedRangeId1, long.Parse(expectedStartIndex1, null), long.Parse(expectedEndIndex1, null));

            // Compare two SequenceRanges which are identical.
            int result = seqRange.CompareTo(secondSeqRange);

            // Validate result of comparison.
            Assert.AreEqual(Convert.ToInt32(expectedResults, null), result);
            ApplicationLog.WriteLine("SequenceRange P1 : Successfully validated the SequenceRange comparison");
        }

        /// <summary>
        ///     Validate Parent Sequence ranges in result sequence range.
        /// </summary>
        /// <param name="resultSeq">Result seq range group.</param>
        /// <param name="refSeq">Reference seq range group.</param>
        /// <param name="querySeq">Query seq range group.</param>
        /// <param name="isParentSeqRangeRequired">Flag to indicate whether result should contain parent seq ranges or not.</param>
        /// <returns>Returns true if the parent seq ranges are valid; otherwise returns false.</returns>
        private static bool ValidateParentSeqRange(SequenceRangeGrouping resultSeq,
                                                   SequenceRangeGrouping refSeq,
                                                   SequenceRangeGrouping querySeq,
                                                   bool isParentSeqRangeRequired)
        {
            IList<ISequenceRange> refSeqRangeList = new List<ISequenceRange>();
            IList<ISequenceRange> querySeqRangeList = new List<ISequenceRange>();

            IEnumerable<string> groupIds = resultSeq.GroupIDs;

            foreach (string groupId in groupIds)
            {
                if (null != refSeq)
                {
                    refSeqRangeList = refSeq.GetGroup(groupId);
                }

                if (null != querySeq)
                {
                    querySeqRangeList = querySeq.GetGroup(groupId);
                }

                foreach (ISequenceRange resultRange in resultSeq.GetGroup(groupId))
                {
                    if (!isParentSeqRangeRequired)
                    {
                        if (0 != resultRange.ParentSeqRanges.Count)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        int refSeqRangeCount = refSeqRangeList.Count(s => resultRange.ParentSeqRanges.Contains(s));
                        int querySeqRangeCount = querySeqRangeList.Count(s => resultRange.ParentSeqRanges.Contains(s));

                        if (resultRange.ParentSeqRanges.Count != refSeqRangeCount + querySeqRangeCount)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion Helper Methods
    }
}