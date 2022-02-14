

 private void _dispatchDomainEvents() {
        
            var domainEventEntities = ChangeTracker.Entries<IEntity>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                foreach (var @event in entity.DomainEvents)
                {
                     DomainEvent.Raise(@event);
                }
                   
            }
}
