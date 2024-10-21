// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace FlatBuffersSetup.Gameplay
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct BuildingSettings : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_24_3_25(); }
  public static BuildingSettings GetRootAsBuildingSettings(ByteBuffer _bb) { return GetRootAsBuildingSettings(_bb, new BuildingSettings()); }
  public static BuildingSettings GetRootAsBuildingSettings(ByteBuffer _bb, BuildingSettings obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public BuildingSettings __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string TypeId { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTypeIdBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetTypeIdBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetTypeIdArray() { return __p.__vector_as_array<byte>(4); }
  public string TitleLid { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTitleLidBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetTitleLidBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetTitleLidArray() { return __p.__vector_as_array<byte>(6); }
  public string BuildingPriceResourceType { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetBuildingPriceResourceTypeBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetBuildingPriceResourceTypeBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetBuildingPriceResourceTypeArray() { return __p.__vector_as_array<byte>(8); }
  public int BuildingPriceValue { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<FlatBuffersSetup.Gameplay.BuildingSettings> CreateBuildingSettings(FlatBufferBuilder builder,
      StringOffset typeIdOffset = default(StringOffset),
      StringOffset titleLidOffset = default(StringOffset),
      StringOffset building_price_resource_typeOffset = default(StringOffset),
      int building_price_value = 0) {
    builder.StartTable(4);
    BuildingSettings.AddBuildingPriceValue(builder, building_price_value);
    BuildingSettings.AddBuildingPriceResourceType(builder, building_price_resource_typeOffset);
    BuildingSettings.AddTitleLid(builder, titleLidOffset);
    BuildingSettings.AddTypeId(builder, typeIdOffset);
    return BuildingSettings.EndBuildingSettings(builder);
  }

  public static void StartBuildingSettings(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddTypeId(FlatBufferBuilder builder, StringOffset typeIdOffset) { builder.AddOffset(0, typeIdOffset.Value, 0); }
  public static void AddTitleLid(FlatBufferBuilder builder, StringOffset titleLidOffset) { builder.AddOffset(1, titleLidOffset.Value, 0); }
  public static void AddBuildingPriceResourceType(FlatBufferBuilder builder, StringOffset buildingPriceResourceTypeOffset) { builder.AddOffset(2, buildingPriceResourceTypeOffset.Value, 0); }
  public static void AddBuildingPriceValue(FlatBufferBuilder builder, int buildingPriceValue) { builder.AddInt(3, buildingPriceValue, 0); }
  public static Offset<FlatBuffersSetup.Gameplay.BuildingSettings> EndBuildingSettings(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<FlatBuffersSetup.Gameplay.BuildingSettings>(o);
  }
  public BuildingSettingsT UnPack() {
    var _o = new BuildingSettingsT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(BuildingSettingsT _o) {
    _o.TypeId = this.TypeId;
    _o.TitleLid = this.TitleLid;
    _o.BuildingPriceResourceType = this.BuildingPriceResourceType;
    _o.BuildingPriceValue = this.BuildingPriceValue;
  }
  public static Offset<FlatBuffersSetup.Gameplay.BuildingSettings> Pack(FlatBufferBuilder builder, BuildingSettingsT _o) {
    if (_o == null) return default(Offset<FlatBuffersSetup.Gameplay.BuildingSettings>);
    var _typeId = _o.TypeId == null ? default(StringOffset) : builder.CreateString(_o.TypeId);
    var _titleLid = _o.TitleLid == null ? default(StringOffset) : builder.CreateString(_o.TitleLid);
    var _building_price_resource_type = _o.BuildingPriceResourceType == null ? default(StringOffset) : builder.CreateString(_o.BuildingPriceResourceType);
    return CreateBuildingSettings(
      builder,
      _typeId,
      _titleLid,
      _building_price_resource_type,
      _o.BuildingPriceValue);
  }
}

public class BuildingSettingsT
{
  public string TypeId { get; set; }
  public string TitleLid { get; set; }
  public string BuildingPriceResourceType { get; set; }
  public int BuildingPriceValue { get; set; }

  public BuildingSettingsT() {
    this.TypeId = null;
    this.TitleLid = null;
    this.BuildingPriceResourceType = null;
    this.BuildingPriceValue = 0;
  }
}


static public class BuildingSettingsVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyString(tablePos, 4 /*TypeId*/, false)
      && verifier.VerifyString(tablePos, 6 /*TitleLid*/, false)
      && verifier.VerifyString(tablePos, 8 /*BuildingPriceResourceType*/, false)
      && verifier.VerifyField(tablePos, 10 /*BuildingPriceValue*/, 4 /*int*/, 4, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}