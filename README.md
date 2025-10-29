@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
public class Alert {
    @Id
    @NotNull (message = "Id can't be null ")
    private String id;
    @NotNull  (message = "Origin can't be null ")
    private String origin;
    @NotNull  (message = "Type can't be null ")
    private AlertTypeEnum type;
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="legal_entity_id")
    @JsonIgnore
    private LegalEntity legalEntity;
    private String content;
    private LocalDateTime createdAt;
    private LocalDateTime updatedAt;
}
this is aler t talbe i want to add it directly to the baseline and i would like to try another wzy to do things by doing with the old baseline and consolidate liquibase command to be able to have all my changesets in my file and then inform my team about both of the approch and both of them are working
