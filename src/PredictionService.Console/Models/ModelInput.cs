using Microsoft.ML.Data;

public class ModelInput
{
    [ColumnName(@"id")]
    public float Id { get; set; }

    [ColumnName(@"Compression1")]
    public float Compression1 { get; set; }

    [ColumnName(@"Compression2")]
    public float Compression2 { get; set; }

    [ColumnName(@"Wait_Time")]
    public float Wait_Time { get; set; }

    [ColumnName(@"Withdrawal1")]
    public float Withdrawal1 { get; set; }

    [ColumnName(@"Withdrawal2")]
    public float Withdrawal2 { get; set; }

    [ColumnName(@"adhesiveness")]
    public float Adhesiveness { get; set; }

    [ColumnName(@"chewiness")]
    public float Chewiness { get; set; }

    [ColumnName(@"cohesiveness")]
    public float Cohesiveness { get; set; }

    [ColumnName(@"depth")]
    public float Depth { get; set; }

    [ColumnName(@"externalHumidity")]
    public float ExternalHumidity { get; set; }

    [ColumnName(@"externalTemperature")]
    public float ExternalTemperature { get; set; }

    [ColumnName(@"fracturability")]
    public float Fracturability { get; set; }

    [ColumnName(@"gumminess")]
    public float Gumminess { get; set; }

    [ColumnName(@"hardness")]
    public float Hardness { get; set; }

    [ColumnName(@"height")]
    public float Height { get; set; }

    [ColumnName(@"idRecipe")]
    public float IdRecipe { get; set; }

    [ColumnName(@"internalHumidity")]
    public float InternalHumidity { get; set; }

    [ColumnName(@"internalTemperature")]
    public float InternalTemperature { get; set; }

    [ColumnName(@"referencePLC")]
    public float ReferencePLC { get; set; }

    [ColumnName(@"resilience")]
    public float Resilience { get; set; }

    [ColumnName(@"springiness")]
    public float Springiness { get; set; }

    [ColumnName(@"volume")]
    public float Volume { get; set; }

    [ColumnName(@"weight")]
    public float Weight { get; set; }

    [ColumnName(@"width")]
    public float Width { get; set; }

    [ColumnName(@"qualityScore")]
    public float QualityScore { get; set; }

}