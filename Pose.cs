﻿using System;
using System.Collections.Generic;

namespace FbxSharp
{
    public class Pose : FbxObject
    {
        public Pose(String name="")
            : base(name)
        {
            PoseInfos = new CollectionView<PoseInfo, PoseInfo>(poseInfos, eh => poseInfos.CollectionChanged += eh);
        }

        #region Public Member Functions

        readonly ChangeNotifyList<PoseInfo> poseInfos = new ChangeNotifyList<PoseInfo>();
        public readonly CollectionView<PoseInfo, PoseInfo> PoseInfos;

        public struct PoseInfo
        {
            public PoseInfo(Node node, Matrix matrix, bool matrixIsLocal=false)
            {
                Node = node;
                Matrix = matrix;
                MatrixIsLocal = matrixIsLocal;
            }

            public readonly Node Node;
            public readonly Matrix Matrix;
            public readonly bool MatrixIsLocal;
        }

        public void SetIsBindPose(bool pIsBindPose)
        {
            isBindPose = pIsBindPose;
        }

        bool isBindPose;
        public bool IsBindPose()
        {
            return isBindPose;
        }

        public bool IsRestPose()
        {
            return !IsBindPose();
        }

        public int GetCount()
        {
            return poseInfos.Count;
        }

        public int Add(Node pNode, Matrix pMatrix, bool pLocalMatrix=false/*, bool pMultipleBindPose=true*/)
        {
            var match = poseInfos.FindIndex(pi => pi.Node == pNode);
            if (match >= 0)
            {
                if (poseInfos[match].Matrix != pMatrix)
                    return -1;
                return match;
            }

            var p = new PoseInfo(pNode, pMatrix, pLocalMatrix);
            poseInfos.Add(p);

            return poseInfos.IndexOf(p);
        }

        public void Remove(int pIndex)
        {
            poseInfos.RemoveAt(pIndex);
        }

        public Node GetNode(int pIndex)
        {
            return poseInfos[pIndex].Node;
        }

        public Matrix GetMatrix(int pIndex)
        {
            return poseInfos[pIndex].Matrix;
        }

        public bool IsLocalMatrix(int pIndex)
        {
            return poseInfos[pIndex].MatrixIsLocal;
        }

        #endregion
    }
}

