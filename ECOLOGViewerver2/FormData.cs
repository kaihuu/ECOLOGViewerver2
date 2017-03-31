using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOLOGViewerver2
{
    public class FormData
    {
        internal string driverID;
        internal string carID;
        internal string sensorID;
        internal string tripID;
        internal string startTime;
        internal string endTime;
        internal string direction;
        internal string consumedenergy;
        internal string aggregation;
        internal string annotation;
        internal string semanticLinkID;
        internal string semantics;
        internal string polyline;

        // 設定用の変数
        internal string value;
        internal string setting;
        internal int PointingDistance;
        internal string averageQuery;
        internal string worstQuery;
        internal string currentDirectory;
        internal string currentFile;
        internal bool usefixed;
        internal bool useNexus7Camera;

        /// <summary>
        /// コンストラクタ1
        /// </summary>
        public FormData()
        {
        }

        /// <summary>
        /// コンストラクタ2
        /// </summary>
        /// <param name="u">参照するインスタンス</param>
        public FormData(FormData u)
        {
            this.driverID = u.driverID;
            this.carID = u.carID;
            this.sensorID = u.sensorID;
            this.tripID = u.tripID;
            this.startTime = u.startTime;
            this.endTime = u.endTime;
            this.direction = u.direction;
            this.consumedenergy = u.consumedenergy;
            this.aggregation = u.aggregation;
            this.value = u.value;
            this.setting = u.setting;
            this.PointingDistance = u.PointingDistance;
            this.averageQuery = u.averageQuery;
            this.worstQuery = u.worstQuery;
            this.currentDirectory = u.currentDirectory;
            this.currentFile = u.currentFile;
            this.annotation = u.annotation;
            this.semanticLinkID = u.semanticLinkID;
            this.semantics = u.semantics;
            this.polyline = u.polyline;
            this.usefixed = u.usefixed;
            this.useNexus7Camera = u.useNexus7Camera;
        }

    }
}
