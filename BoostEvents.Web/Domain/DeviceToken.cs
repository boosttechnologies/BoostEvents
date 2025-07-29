using System.ComponentModel.DataAnnotations;

namespace BoostEvents.Web.Domain;

public class DeviceToken : BaseEntity
{
    [Required]
    public Guid BusinessInstanceId { get; set; }

    [Required]
    public DeviceType DeviceType { get; set; } // Enum: POS, AccessControl, Kiosk

    [Required]
    [MaxLength(200)]
    public required string SerialNumber { get; set; } // Hardware-linked to prevent impersonation

    /// <summary>
    /// Encrypted device configuration payload (e.g. endpoint, encryption keys, mode).
    /// </summary>
    [MaxLength(2048)]
    public required string SettingsEncryptedJson { get; set; }

    /// <summary>
    /// Manual, Auth0, NFC — defines how the device was paired.
    /// </summary>
    [MaxLength(50)]
    public required string PairingMethod { get; set; }

    /// <summary>
    /// Optional zone or gate this device is assigned to.
    /// </summary>
    [MaxLength(100)]
    public required string ZoneId { get; set; }

    public Guid? PairedByUserId { get; set; }

}