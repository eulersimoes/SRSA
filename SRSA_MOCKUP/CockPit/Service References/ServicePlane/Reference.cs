﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17379
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace COCKPIT.ServicePlane {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicePlane.PlaneInfoSoap")]
    public interface PlaneInfoSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/OpenSerialPort", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string OpenSerialPort(string CommPort);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CloseSerialPort", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string CloseSerialPort(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPlaneInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetPlaneInfo(int Flight);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StartAutomaticGetPlaneInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void StartAutomaticGetPlaneInfo(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StopAutomaticGetPlaneInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void StopAutomaticGetPlaneInfo(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LigarStrobes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LigarStrobes(bool Valor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LigarLuzesNav", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LigarLuzesNav(bool Valor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LigarFarol", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LigarFarol(bool Valor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddHomePoint", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AddHomePoint(int Id, int LatitudeE6, double LongitudeE6, int Altitude);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddWayPoint", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AddWayPoint(int Id, int LatitudeE6, double LongitudeE6, int Altitude);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RemoverWayPoint", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string RemoverWayPoint(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RemoverWayPoints", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string RemoverWayPoints(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarWayPointsStringE6", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarWayPointsStringE6(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarWayPointsStringE6Sync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarWayPointsStringE6Sync(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarWayPoints", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        COCKPIT.ServicePlane.WayPoint[] ListarWayPoints(string Request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SyncPoints", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SyncPoints(string Sync);
        
        // CODEGEN: Parameter 'GetCamPicResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCamPic", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        COCKPIT.ServicePlane.GetCamPicResponse GetCamPic(COCKPIT.ServicePlane.GetCamPicRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IniciarCamera", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void IniciarCamera();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/FinalizarCamera", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void FinalizarCamera();
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17379")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class WayPoint : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private double latitudeField;
        
        private double longitudeField;
        
        private double altitudeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public double Latitude {
            get {
                return this.latitudeField;
            }
            set {
                this.latitudeField = value;
                this.RaisePropertyChanged("Latitude");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public double Longitude {
            get {
                return this.longitudeField;
            }
            set {
                this.longitudeField = value;
                this.RaisePropertyChanged("Longitude");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public double Altitude {
            get {
                return this.altitudeField;
            }
            set {
                this.altitudeField = value;
                this.RaisePropertyChanged("Altitude");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetCamPic", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetCamPicRequest {
        
        public GetCamPicRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetCamPicResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetCamPicResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetCamPicResult;
        
        public GetCamPicResponse() {
        }
        
        public GetCamPicResponse(byte[] GetCamPicResult) {
            this.GetCamPicResult = GetCamPicResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PlaneInfoSoapChannel : COCKPIT.ServicePlane.PlaneInfoSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PlaneInfoSoapClient : System.ServiceModel.ClientBase<COCKPIT.ServicePlane.PlaneInfoSoap>, COCKPIT.ServicePlane.PlaneInfoSoap {
        
        public PlaneInfoSoapClient() {
        }
        
        public PlaneInfoSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PlaneInfoSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlaneInfoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlaneInfoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string OpenSerialPort(string CommPort) {
            return base.Channel.OpenSerialPort(CommPort);
        }
        
        public string CloseSerialPort(string Request) {
            return base.Channel.CloseSerialPort(Request);
        }
        
        public string GetPlaneInfo(int Flight) {
            return base.Channel.GetPlaneInfo(Flight);
        }
        
        public void StartAutomaticGetPlaneInfo(string Request) {
            base.Channel.StartAutomaticGetPlaneInfo(Request);
        }
        
        public void StopAutomaticGetPlaneInfo(string Request) {
            base.Channel.StopAutomaticGetPlaneInfo(Request);
        }
        
        public string LigarStrobes(bool Valor) {
            return base.Channel.LigarStrobes(Valor);
        }
        
        public string LigarLuzesNav(bool Valor) {
            return base.Channel.LigarLuzesNav(Valor);
        }
        
        public string LigarFarol(bool Valor) {
            return base.Channel.LigarFarol(Valor);
        }
        
        public string AddHomePoint(int Id, int LatitudeE6, double LongitudeE6, int Altitude) {
            return base.Channel.AddHomePoint(Id, LatitudeE6, LongitudeE6, Altitude);
        }
        
        public string AddWayPoint(int Id, int LatitudeE6, double LongitudeE6, int Altitude) {
            return base.Channel.AddWayPoint(Id, LatitudeE6, LongitudeE6, Altitude);
        }
        
        public string RemoverWayPoint(int id) {
            return base.Channel.RemoverWayPoint(id);
        }
        
        public string RemoverWayPoints(string Request) {
            return base.Channel.RemoverWayPoints(Request);
        }
        
        public string ListarWayPointsStringE6(string Request) {
            return base.Channel.ListarWayPointsStringE6(Request);
        }
        
        public string ListarWayPointsStringE6Sync(string Request) {
            return base.Channel.ListarWayPointsStringE6Sync(Request);
        }
        
        public COCKPIT.ServicePlane.WayPoint[] ListarWayPoints(string Request) {
            return base.Channel.ListarWayPoints(Request);
        }
        
        public string SyncPoints(string Sync) {
            return base.Channel.SyncPoints(Sync);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        COCKPIT.ServicePlane.GetCamPicResponse COCKPIT.ServicePlane.PlaneInfoSoap.GetCamPic(COCKPIT.ServicePlane.GetCamPicRequest request) {
            return base.Channel.GetCamPic(request);
        }
        
        public byte[] GetCamPic() {
            COCKPIT.ServicePlane.GetCamPicRequest inValue = new COCKPIT.ServicePlane.GetCamPicRequest();
            COCKPIT.ServicePlane.GetCamPicResponse retVal = ((COCKPIT.ServicePlane.PlaneInfoSoap)(this)).GetCamPic(inValue);
            return retVal.GetCamPicResult;
        }
        
        public void IniciarCamera() {
            base.Channel.IniciarCamera();
        }
        
        public void FinalizarCamera() {
            base.Channel.FinalizarCamera();
        }
    }
}